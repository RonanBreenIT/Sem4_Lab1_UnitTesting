// CA3 sample solution - bank account
// author: GC

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
//using UnitTestBankAccount1;

namespace Bank
{
    // a bank account

    // d. In order to test the transaction history you will also need to make the BankAccount class enumerable.
    public class BankAccount: IEnumerable // Take out the bank account
    {
        // fields
        private String sortCode;
        private String accountNo;
        private double balance;                 // €
        private double overdraftLimit;          // €  

        /* a. Add read-only public properties to the BankAccount which correspond to the sort code, account no, balance, and overdraft limit */
        public String SortCode { get { return this.sortCode; } }
        
        public String AccountNo { get { return this.accountNo; } }

        public double Balance
        {
            get
            {
                return this.balance;
            }
        }

        public double OverdraftLimit
        {
            get
            {
                return this.overdraftLimit;
            }
        }

        private const int MaxTransactions = 100;
        public double[] transactionHistory;   // a record of amounts deposited and withdrawn
        private int nextTransaction;           // the next free slot in the transactionHistory array 

        

        // constructor
        public BankAccount(String sortCode, String accountNo, double overdraftLimit)
        {
            this.sortCode = sortCode;
            this.accountNo = accountNo;
            this.balance = 0;
            if (overdraftLimit <= 0) // Added this for negative overdraft limit test
            {
                this.overdraftLimit = 0;
            }
            else
            {
                this.overdraftLimit = overdraftLimit;
            }

            transactionHistory = new double[MaxTransactions];
            nextTransaction = 0;                // no transaction to date
            
        }

        // overloaded constructor - chain
        public BankAccount(String sortCode, String accountNo)
            : this(sortCode, accountNo, 0)
        {
        }


        // deposit money in the account
        public void Deposit(double amount)                      // assume amount is positive
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Cant make negative deposit");
            }
            else
            {
                balance += amount;

                // record in transaction history
                transactionHistory[nextTransaction++] = amount;
            }
            
        }

        // withdraw money if there are sufficient funds
        public void Withdraw(double amount)                     // assume amount is positive
        {
            if ((balance + overdraftLimit) > amount)
            {
                balance -= amount;
                transactionHistory[nextTransaction++] = amount * -1;                // Makes value a minus figure
                //return true;  changed return to Bool * RB                          // withdraw was succesful
            }
            else                                        // unsufficient funds
            {
                // C. Have the Withdraw method throw an exception if there are insufficient funds in the account
                throw new ArgumentException("Insufficent funds in the account");
            }
        }

        // print account details to the console
        //public void PrintAccountDetails()
        //{
        //    Console.WriteLine("sort code: " + sortCode + " account no: " + accountNo + " overdraft limit: " + overdraftLimit + " balance: " + balance);
        //}

        //// print the transaction history
        //public void PrintTransactionHistory()
        //{
        //    Console.WriteLine("Transaction History:");
        //    for (int i = 0; i < nextTransaction; i++ )
        //    {
        //        Console.Write(transactionHistory[i] + " ");
        //    }
        //    Console.WriteLine();
        //}

        /* b. Remove the print methods(for the account and transaction history) and override ToString() instead to 
         * return all information about the account including the transaction history */

        public override string ToString()
        {
            Console.WriteLine("----- Transaction History -----");
            StringBuilder sb1 = new StringBuilder();
            sb1.Append("\nsort code: " + sortCode + "\naccount no: " + accountNo + "\noverdraft limit: " + overdraftLimit + "\nbalance: " + balance);
            sb1.Append("\nTransaction History: ");
            for (int i = 0; i < nextTransaction; i++)
            {
                sb1.Append("Transaction " + (i + 1) + " = " + transactionHistory[i] + " \n");
            }               
            return sb1.ToString();
        }


        public IEnumerator GetEnumerator()
        {
            //foreach (var item in transactionHistory)
            //{
            //    yield return item;
            //}
            //or
            for (int i = 0; i < nextTransaction; i++)
            {
                yield return transactionHistory[i];
            }
        }

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}

       
    }

    

    // test class
    class BankAccountTest
    {
        //public static void Main()
        //{
        //    BankAccount b = new BankAccount("903555", "12344544", 1000);
        //    //b.PrintAccountDetails();

        //    b.Deposit(100);

        //    if (b.Withdraw(200))
        //    {
        //        Console.WriteLine("Withdrawal was successful");
        //        b.PrintAccountDetails();
        //    }
        //    else
        //    {
        //        Console.WriteLine("Withdrawal failed");
        //    }

        //    b.Deposit(500);
        //    b.PrintAccountDetails();
        //    b.PrintTransactionHistory();
        //}
    }
}
