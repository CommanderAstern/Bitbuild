// HelloWorld.cdc
//
// Welcome to Cadence! This is one of the simplest programs you can deploy on Flow.
//
// The HelloWorld contract contains a single string field and a public getter function.
//
// Follow the "Hello, World!" tutorial to learn more: https://docs.onflow.org/cadence/tutorial/02-hello-world/

access(all) contract BasicInventoryV3 {

    // Declare a public field of type String.
    //
    // All fields must be initialized in the init() function.
    access(all) let greeting: String
    access(all) let addressHasItem: {Address: [Bool]}

    // The init() function is required if the contract contains any fields.
    init() {
        self.greeting = "Hello, World!"
        self.addressHasItem = {}
    }
    // check if address is in database
    access(all) fun inDatabase(_address:Address): Bool {
        return self.addressHasItem.containsKey(_address)
    }
    pub fun initializeAccount(_address: Address): Bool{
        pre {
            !self.addressHasItem.containsKey(_address)
        }
        self.addressHasItem[_address] = [false, false, false, false, false]
        return true;
    }

    pub fun enableOwnership(_address: Address, _itemID: Int): Bool{
        pre{
            _itemID<=4
        }
        let temp = self.addressHasItem[_address] ?? []
        temp[_itemID] = true
        self.addressHasItem[_address] = temp
        return true
    }
    access(all) fun getItemsList(_address: Address): [Bool]{
        return self.addressHasItem[_address] ?? []
    }

    // Public function that returns our friendly greeting!
    access(all) fun hello(): String {
        return self.greeting
    }
}
