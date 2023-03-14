use anchor_lang::prelude::*;

declare_id!("8NjoFioVCkFgz2mLLf8L2BpwJ4hopAaYhADcEhJ5P5cn");

#[program]
pub mod pdas {
    use super::*;

    pub fn create_ledger(ctx: Context<CreateLedger>, name: String, bio: String) -> Result<()> {
        let ledger_account = &mut ctx.accounts.ledger_account;
        let wallet = &mut ctx.accounts.wallet;

        ledger_account.name = name;
        ledger_account.bio = bio;
        ledger_account.wallet = *wallet.key;
        Ok(())
    }

    pub fn modify_ledger(ctx: Context<ModifyLedger>, name: String, bio: String) -> Result<()> {
        let ledger_account = &mut ctx.accounts.ledger_account;
        ledger_account.name = name;
        ledger_account.bio = bio;

        Ok(())
    }
}

#[derive(Accounts)]
#[instruction(color: String)]
pub struct CreateLedger<'info> {
    #[account(
        init,
        payer = wallet,
        space = 500,
        seeds = 
        [
            wallet.key().as_ref(),
        ],
        bump
    )]
    pub ledger_account: Account<'info, Ledger>,
    #[account(mut)]
    pub wallet: Signer<'info>,
    pub system_program: Program<'info, System>,
}

#[derive(Accounts)]
pub struct ModifyLedger<'info> {
    #[account(mut)]
    pub ledger_account: Account<'info, Ledger>,
    #[account(mut)]
    pub wallet: Signer<'info>,
}

#[account]
pub struct Ledger {
    pub name: String,
    pub bio: String,
    pub wallet: Pubkey,
}
