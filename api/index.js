var express = require("express");
var app = express();
const { Program, AnchorProvider, BN, web3 } = require('@project-serum/anchor');
const { Connection, clusterApiUrl, PublicKey, Keypair } = require('@solana/web3.js');
const { Metaplex, keypairIdentity, bundlrStorage, toMetaplexFile, toBigNumber, CreateCandyMachineInput, DefaultCandyGuardSettings, CandyMachineItem, toDateTime, sol, TransactionBuilder, CreateCandyMachineBuilderContext } =  require("@metaplex-foundation/js");
const bs58 = require('bs58');

const CANDY_MACHINE_ADDRESS = 'LPyt8g7m96HN4XF7ShBKf5Z3Pp7PbaECZWwRDRDYcwt';
const COLLECTION_ADDRESS = "8EqVZxRstt3oVfxnEkQssDrCNFX5jNxYnMvp7Tt1b4dn";

const WALLET = Keypair.fromSecretKey(new Uint8Array([142,22,131,43,13,8,195,223,58,97,14,198,114,96,93,224,217,8,214,194,129,36,100,8,60,174,1,110,153,235,139,203,0,120,34,158,223,14,131,160,82,6,217,95,10,175,140,163,218,168,41,24,140,226,3,150,87,166,176,140,159,27,196,34]));
const network = clusterApiUrl("devnet")
const connection = new Connection(network);
const METAPLEX = Metaplex.make(connection)
    .use(keypairIdentity(WALLET));


app.get("/getOwnedNFT", async (req, res, next) => {

    const ownerAddress = req.query.ownerAddress;
    if (ownerAddress == null) {
        return res.status(400).json({ message: "Missing required parameters" });
    }

    try {
        const candyMachine = await METAPLEX
        .candyMachines()
        .findByAddress({ address: new PublicKey(CANDY_MACHINE_ADDRESS) }); 
        const myNfts = await METAPLEX.nfts().findAllByOwner({
            owner: new PublicKey(ownerAddress)
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
    const privateKey = req.query.privateKey;
    if (privateKey == null) {
        return res.status(400).json({ message: "Missing required parameters" });
    }
    try {
        wallet = Keypair.fromSecretKey(new Uint8Array(bs58.decode(privateKey)));
        const candyMachine = await METAPLEX
            .candyMachines()
            .findByAddress({ address: new PublicKey("LPyt8g7m96HN4XF7ShBKf5Z3Pp7PbaECZWwRDRDYcwt") }); 
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
        console.log("NFT")
        console.log(`✅ - Minted NFT: ${nft.address.toString()}`);
        res.json({"nftAddress": nft.address.toString(), "txAddress":response.signature});
        
    } catch (error) {
        console.log(error);
        res.json({"error": error}); 
    }

    // console.log(`     https://explorer.solana.com/address/${nft.address.toString()}?cluster=devnet`);
    // console.log(`     https://explorer.solana.com/tx/${response.signature}?cluster=devnet`);
});


app.listen(3000, () => {
 console.log("Server running on port 3000");
});
