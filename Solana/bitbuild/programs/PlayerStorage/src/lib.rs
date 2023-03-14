use anchor_lang::prelude::*;

// This is your program's public key and it will update
// automatically when you build the project.
declare_id!("8NjoFioVCkFgz2mLLf8L2BpwJ4hopAaYhADcEhJ5P5cn");

#[program]
pub mod bitbuild {
    use super::*;

    pub fn create_inventory(
        ctx: Context<CreateInventory>,
        username: String,
        bio: String,
    ) -> Result<()> {
        let inventory = &mut ctx.accounts.inventory;
        let user = &mut ctx.accounts.user;

        inventory.username = username;
        inventory.bio = bio;
        inventory.user = *user.key;

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
    pub user: AccountInfo<'info>,
    pub signer: Signer<'info>,
    pub system_program: Program<'info, System>,
}

#[account]
pub struct Inventory {
    pub username: String,
    pub bio: String,
    pub user: Pubkey,
}
