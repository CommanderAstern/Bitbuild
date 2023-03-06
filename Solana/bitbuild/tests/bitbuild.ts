import * as anchor from "@project-serum/anchor";
import { Program } from "@project-serum/anchor";
import { assert } from "chai";
import { Bitbuild } from "../target/types/bitbuild";

describe("bitbuild", () => {
    it("can create item", async () => {
      let inventoryKp = new web3.Keypair();
      const txHash = await pg.program.methods
        .createInventory(12)
        .accounts({
          inventory: inventoryKp.publicKey,
          user: pg.wallet.publicKey,
          systemProgram: anchor.web3.SystemProgram.programId,
        })
        .signers([inventoryKp])
        .rpc();
  
      console.log(`Use 'solana confirm -v ${txHash}' to see the logs`);
      await pg.connection.confirmTransaction(txHash);
  
      const newInventory = await pg.program.account.inventory.fetch(
        inventoryKp.publicKey
      );
  
      console.log("On-chain data is:", newInventory.itemId.toString());
      console.log(await pg.program.account.inventory.all())
      assert.strictEqual(newInventory.itemId, 12);
    });
  });
  