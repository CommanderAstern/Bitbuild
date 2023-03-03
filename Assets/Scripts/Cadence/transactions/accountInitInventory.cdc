import BasicInventoryV3 from 0x91c7fd71eaa35f2f

transaction {

  prepare(acct: AuthAccount) {}

  execute {
    log(BasicInventoryV3.initializeAccount(_address: 0x01))
  }
}
