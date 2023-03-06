var express = require("express");
var app = express();
const { Program, AnchorProvider, BN, web3 } = require('@project-serum/anchor');
const { Connection, clusterApiUrl, PublicKey, Keypair } = require('@solana/web3.js');
const idl = require('../Solana/bitbuild/target/idl/bitbuild.json');
const opts = {
    preflightCommitment: "recent",
  };
  
const { SystemProgram } = web3
const programID = new PublicKey('F83Qnc2QkcVXwrP4MxKMn9nZmqek8ZeWmXvNbaMg88Xu');

// app.get("/url", (req, res, next) => {
//  res.json(["Tony","Lisa","Michael","Ginger","Food"]);
// });

// app.listen(3000, () => {
//  console.log("Server running on port 3000");
// });

checkVal()

async function checkVal(params) {
    const wallet = Keypair.fromSecretKey(
        Uint8Array.from([142,22,131,43,13,8,195,223,58,97,14,198,114,96,93,224,217,8,214,194,129,36,100,8,60,174,1,110,153,235,139,203,0,120,34,158,223,14,131,160,82,6,217,95,10,175,140,163,218,168,41,24,140,226,3,150,87,166,176,140,159,27,196,34])
    );
    const network = clusterApiUrl("devnet")
    const connection = new Connection(network);

    const provider = new AnchorProvider(
        connection, wallet, { commitment: "processed" },
    )
    
    const program = new Program(idl, programID, provider);

    var obj = await program.account.inventory.all();
    console.log(obj);
}