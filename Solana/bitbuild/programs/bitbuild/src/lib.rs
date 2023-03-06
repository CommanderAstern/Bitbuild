use anchor_lang::prelude::*;

// This is your program's public key and it will update
// automatically when you build the project.
declare_id!("73kyB7up83mfndcKpn4s49ZJMsKZZ8RzeQLwWduLcuf2");

#[program]
pub mod bitbuild {
    use super::*;

    pub fn create_inventory(ctx: Context<CreateInventory>, item_id: u32) -> Result<()> {
        let inventory = &mut ctx.accounts.inventory;
        let user = &mut ctx.accounts.user;

        inventory.item_id = item_id;
        inventory.user = *user.key;

        msg!("Item ID: {}!", inventory.item_id);
        msg!("User ID: {}!", inventory.user);
        Ok(())
    }
}

#[derive(Accounts)]
pub struct CreateInventory<'info> {
    #[account(
        init,
        payer = user,
        space = 2000
    )]
    pub inventory: Account<'info, Inventory>,

    #[account(mut)]
    pub user: Signer<'info>,
    pub system_program: Program<'info, System>,
}

#[account]
pub struct Inventory {
    pub item_id: u32,
    pub user: Pubkey,
}
