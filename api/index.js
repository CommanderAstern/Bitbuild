var express = require("express");
var app = express();
const { Program, AnchorProvider, BN, web3, workspace, setProvider} = require('@project-serum/anchor');
const { Connection, clusterApiUrl } = require('@solana/web3.js');
const { Metaplex, keypairIdentity, bundlrStorage, toMetaplexFile, toBigNumber, CreateCandyMachineInput, DefaultCandyGuardSettings, CandyMachineItem, toDateTime, sol, TransactionBuilder, CreateCandyMachineBuilderContext } =  require("@metaplex-foundation/js");
const idl = require('./pdas.json');
const bs58 = require('bs58');

const CANDY_MACHINE_ADDRESS = 'EJTJ15D4ji3Rx22gdGrNadPUXi9aZTCnWPhUm9garuXK';
const COLLECTION_ADDRESS = "6SuWuSeV83r8TUKtkxnY7WEsdPq9HzhRfxK9nZdzvi6R";
const PROGRAM_ID = "8NjoFioVCkFgz2mLLf8L2BpwJ4hopAaYhADcEhJ5P5cn";

const WALLET = web3.Keypair.fromSecretKey(new Uint8Array([142,22,131,43,13,8,195,223,58,97,14,198,114,96,93,224,217,8,214,194,129,36,100,8,60,174,1,110,153,235,139,203,0,120,34,158,223,14,131,160,82,6,217,95,10,175,140,163,218,168,41,24,140,226,3,150,87,166,176,140,159,27,196,34]));
const network = clusterApiUrl("devnet")
const connection = new Connection(network);
const METAPLEX = Metaplex.make(connection)
    .use(keypairIdentity(WALLET));

const provider = new AnchorProvider(
    connection,
    WALLET,
    AnchorProvider.defaultOptions()
    );



async function derivePda(pubkey) {
    setProvider(provider);
    const program = new Program(idl, PROGRAM_ID, provider)
    let pubKeyBuffer = new web3.PublicKey(pubkey).toBuffer();
    let [pda, _] = await web3.PublicKey.findProgramAddress(
      [
        pubKeyBuffer
      ],
      program.programId
    );
    return pda;
  }

  async function modifyLedger(
    name,
    bio,
    privateKey, 
  ) {
    try {
        wallet = web3.Keypair.fromSecretKey(new Uint8Array(bs58.decode(privateKey)));
        console.log("--------------------------------------------------");
        let data;
        let pda = await derivePda(wallet.publicKey);
        const providerV2 = new AnchorProvider(connection, wallet, AnchorProvider.defaultOptions());
        setProvider(providerV2);
        const programV2 = new Program(idl, PROGRAM_ID, providerV2);
        console.log(`Checking if account exists`);
        try {
    
          data = await programV2.account.ledger.fetch(pda);
          console.log("It does.");
        
        } catch (e) {
        
          console.log("It does NOT. Creating...");
          await createLedgerAccount(name, bio, pda, wallet);
          data = await programV2.account.ledger.fetch(pda);
        };
        await programV2.methods.modifyLedger(name,bio)
          .accounts({
            ledgerAccount: pda,
            wallet: wallet.publicKey,
          })
          .signers([wallet.keypair])
          .rpc()
          data = await programV2.account.ledger.fetch(pda);
          console.log("New Data:");
          console.log(`${data.name}   Balance: ${data.bio}`);
          console.log("Success.");
          console.log("Success.");
        
    } catch (error) {
        console.log(error);
        return;
    }

  }

app.get("/getOwnedNFT", async (req, res, next) => {

    const ownerAddress = req.query.ownerAddress;
    if (ownerAddress == null) {
        return res.status(400).json({ message: "Missing required parameters" });
    }

    try {
        const candyMachine = await METAPLEX
        .candyMachines()
        .findByAddress({ address: new web3.PublicKey(CANDY_MACHINE_ADDRESS) }); 
        const myNfts = await METAPLEX.nfts().findAllByOwner({
            owner: new web3.PublicKey(ownerAddress)
        });
        var nfts = [];
        for (i in myNfts) {
            if (myNfts[i] && myNfts[i].collection) {
                if (myNfts[i].collection.address.toString() == COLLECTION_ADDRESS) {
                    let nft = await METAPLEX.nfts().load({ "metadata": myNfts[i] });
                    nfts.push(nft.json);
                }
              }
        }
        console.log("NFT fetched for " + ownerAddress);
        res.json(nfts);
        
    } catch (error) {
        console.log(error);
        res.json({"error": error});
    }

});

app.get("/mintNFT", async (req, res, next) => {

    await METAPLEX.nfts().bui


    const privateKey = req.query.privateKey;
    if (privateKey == null) {
        return res.status(400).json({ message: "Missing required parameters" });
    }
    try {
        wallet = web3.Keypair.fromSecretKey(new Uint8Array(bs58.decode(privateKey)));
        console.log(wallet.publicKey)
        const candyMachine = await METAPLEX
            .candyMachines()
            .findByAddress({ address: new web3.PublicKey(CANDY_MACHINE_ADDRESS) }); 
        let { nft, response } = await METAPLEX.candyMachines().mint(
            {
                candyMachine,
                collectionUpdateAuthority: WALLET.publicKey,
                owner: wallet.publicKey,
            },
            { 
                payer: wallet
            },
            {
                commitment:'finalized'
            }
        )
        console.log(nft)
        console.log(`✅ - Minted NFT: ${nft.address.toString()}`);
        res.json({"nftAddress": nft.address.toString(), "txAddress":response.signature, "id": parseInt(nft.json.attributes[0].value)});
        
    } catch (error) {
        console.log(error);
        res.json({"error": error}); 
    }

    // console.log(`     https://explorer.solana.com/address/${nft.address.toString()}?cluster=devnet`);
    // console.log(`     https://explorer.solana.com/tx/${response.signature}?cluster=devnet`);
});

app.get("/getPlayerInfo", async (req, res, next) => {
    const publicKey = req.query.publicKey;
    if (publicKey == null) {
        return res.status(400).json({ message: "Missing required parameters" });
    }
    try {
        setProvider(provider);
        const program = new Program(idl, PROGRAM_ID, provider)
        let pda = await derivePda(publicKey);
        let data = await program.account.ledger.fetch(pda);
        res.json({"name":data.name,"bio":data.bio});
      
      } catch (error) {
        console.log(error);
        res.json({"error": error}); 
      };

});

app.get("/setPlayerInfo", async (req, res, next) => {
    const privateKey = req.query.privateKey;
    const name = req.query.name;
    const bio = req.query.bio;
    if (privateKey == null || name == null || bio == null ) {
        return res.status(400).json({ message: "Missing required parameters" });
    }
    try {
        modifyLedger(name, bio, privateKey);
    } catch (error) {
        console.log(error);
        res.json({"error": error}); 
    }
    

});


app.listen(3000, () => {
 console.log("Server running on port 3000");
});
