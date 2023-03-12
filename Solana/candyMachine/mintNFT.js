const { Metaplex, keypairIdentity, bundlrStorage, toMetaplexFile, toBigNumber, CreateCandyMachineInput, DefaultCandyGuardSettings, CandyMachineItem, toDateTime, sol, TransactionBuilder, CreateCandyMachineBuilderContext } =  require("@metaplex-foundation/js");
const { Connection, clusterApiUrl, Keypair, PublicKey } =  require("@solana/web3.js");

const COLLECTION_ADDRESS = "8EqVZxRstt3oVfxnEkQssDrCNFX5jNxYnMvp7Tt1b4dn";
const WALLET = Keypair.fromSecretKey(new Uint8Array([142,22,131,43,13,8,195,223,58,97,14,198,114,96,93,224,217,8,214,194,129,36,100,8,60,174,1,110,153,235,139,203,0,120,34,158,223,14,131,160,82,6,217,95,10,175,140,163,218,168,41,24,140,226,3,150,87,166,176,140,159,27,196,34]));
const WALLETV2 = Keypair.fromSecretKey(new Uint8Array([207,183,74,143,1,63,137,50,44,138,250,18,91,122,35,142,190,195,153,20,214,237,110,180,92,139,87,85,63,189,242,118,125,43,4,99,178,186,131,26,174,86,218,117,40,254,218,132,127,111,97,119,99,84,139,107,43,178,9,65,227,178,13,33]));
const network = clusterApiUrl("devnet")
const connection = new Connection(network);
const METAPLEX = Metaplex.make(connection)
    .use(keypairIdentity(WALLET));

async function checkNFT(candy_machine_address, owner_address) {
    const candyMachine = await METAPLEX
    .candyMachines()
    .findByAddress({ address: new PublicKey(candy_machine_address) }); 
    const myNfts = await METAPLEX.nfts().findAllByOwner({
        owner: new PublicKey(owner_address)
    });
    var nfts = [];
    for (i in myNfts) {
        if (myNfts[i] && myNfts[i].collection) {
            if (myNfts[i].collection.address.toString() == COLLECTION_ADDRESS) {
                let nft = await METAPLEX.nfts().load({ "metadata": myNfts[i] });
                nfts.push(nft.json);
            }
          }
        // console.log(myNfts[i].mintAddress == new PublicKey("AQjurmTCsbaNQVzKAiopB37Zgpynus24CjivnxaoPVmy"));
    }
    // var nfts = [];
    // for(i in myNfts) {
    //     let temp = myNfts[i];
    //     const nft = await METAPLEX.nfts().load({ "metadata": temp });
    //     nfts.push(nft);
    // }
    console.log(nfts);
}
checkNFT("LPyt8g7m96HN4XF7ShBKf5Z3Pp7PbaECZWwRDRDYcwt", "9Rc1PtEDtzAhXeGYJZJZxJBvF7YF85L7vDc2BTpsxNCQ");

// async function mintNft() {
//     const candyMachine = await METAPLEX
//         .candyMachines()
//         .findByAddress({ address: new PublicKey("LPyt8g7m96HN4XF7ShBKf5Z3Pp7PbaECZWwRDRDYcwt") }); 
//     let { nft, response } = await METAPLEX.candyMachines().mint(
//         {
//             candyMachine,
//             collectionUpdateAuthority: WALLET.publicKey,
//             owner: WALLETV2.publicKey,
//         },
//         { 
//             payer: WALLETV2
//         },
//         {
//             commitment:'finalized'
//         }
//     )

//     console.log(`✅ - Minted NFT: ${nft.address.toString()}`);
//     console.log(`     https://explorer.solana.com/address/${nft.address.toString()}?cluster=devnet`);
//     console.log(`     https://explorer.solana.com/tx/${response.signature}?cluster=devnet`);
// }

// mintNft()
// mintNft()

