const { Metaplex, keypairIdentity, bundlrStorage, toMetaplexFile, toBigNumber, CreateCandyMachineInput, DefaultCandyGuardSettings, CandyMachineItem, toDateTime, sol, TransactionBuilder, CreateCandyMachineBuilderContext } =  require("@metaplex-foundation/js");
const { Connection, clusterApiUrl, Keypair, PublicKey } =  require("@solana/web3.js");

const WALLET = Keypair.fromSecretKey(new Uint8Array([142,22,131,43,13,8,195,223,58,97,14,198,114,96,93,224,217,8,214,194,129,36,100,8,60,174,1,110,153,235,139,203,0,120,34,158,223,14,131,160,82,6,217,95,10,175,140,163,218,168,41,24,140,226,3,150,87,166,176,140,159,27,196,34]));
const network = clusterApiUrl("devnet")
const connection = new Connection(network);
const METAPLEX = Metaplex.make(connection)
    .use(keypairIdentity(WALLET));

async function checkNFT() {
    const myNfts = await METAPLEX.nfts().findAllByOwner({
        owner: METAPLEX.identity().publicKey
    });
    console.log(myNfts);
}
checkNFT();

// async function mintNft() {
//     const candyMachine = await METAPLEX
//         .candyMachines()
//         .findByAddress({ address: new PublicKey("3Cmd9XhuKdEX1hoiefkEyv7dLEcJByiBxte2yi3JCyRT") }); 
//     let { nft, response } = await METAPLEX.candyMachines().mint({
//         candyMachine,
//         collectionUpdateAuthority: WALLET.publicKey,
//         },{commitment:'finalized'})

//     console.log(`✅ - Minted NFT: ${nft.address.toString()}`);
//     console.log(`     https://explorer.solana.com/address/${nft.address.toString()}?cluster=devnet`);
//     console.log(`     https://explorer.solana.com/tx/${response.signature}?cluster=devnet`);
// }

// mintNft()