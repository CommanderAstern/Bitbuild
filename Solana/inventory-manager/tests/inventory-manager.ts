// No imports needed: web3, anchor, pg and more are globally available

function shortKey(key: anchor.web3.PublicKey) {
  return key.toString().substring(0, 8);
}

describe("pdas", () => {
  async function generateKeypair() {
    return pg.wallet.keypair;
  }
  async function derivePda(pubkey: anchor.web3.PublicKey) {
    let [pda, _] = await anchor.web3.PublicKey.findProgramAddress(
      [pubkey.toBuffer()],
      pg.program.programId
    );
    return pda;
  }

  async function createLedgerAccount(
    name: string,
    bio: string,
    pda: anchor.web3.PublicKey
  ) {
    await pg.program.methods
      .createLedger(name, bio)
      .accounts({
        ledgerAccount: pda,
        wallet: pg.wallet.publicKey,
      })
      .signers([pg.wallet.keypair])
      .rpc();
  }

  async function modifyLedger(name: string, bio: string) {
    console.log("--------------------------------------------------");
    let data;
    let pda = await derivePda(pg.wallet.publicKey);

    console.log(`Checking if account ${shortKey(pda)} exists`);
    try {
      data = await pg.program.account.ledger.fetch(pda);
      console.log("It does.");
    } catch (e) {
      console.log("It does NOT. Creating...");
      await createLedgerAccount(name, bio, pda);
      data = await pg.program.account.ledger.fetch(pda);
    }

    console.log("Success.");
    console.log("Data:");
    console.log(`    Color: ${data.name}   Balance: ${data.bio}`);
    console.log(`Modifying  ${name} ${bio}`);

    await pg.program.methods
      .modifyLedger(name, bio)
      .accounts({
        ledgerAccount: pda,
        wallet: pg.wallet.publicKey,
      })
      .signers([pg.wallet.keypair])
      .rpc();

    data = await pg.program.account.ledger.fetch(pda);
    console.log("New Data:");
    console.log(`${data.name}   Balance: ${data.bio}`);
    console.log("Success.");
  }

  it("initialize", async () => {
    modifyLedger("10", "20");
    modifyLedger("1000", "20009");
    modifyLedger("Cool", "Tool");
    modifyLedger("Dool", "Wool");
  });
});
