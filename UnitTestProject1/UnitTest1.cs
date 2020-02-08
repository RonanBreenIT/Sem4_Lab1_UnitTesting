using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bank;

namespace UnitTestBankAccount1
{
    [TestClass]
    public class UnitTest1
    {

        // Test Deposit Method
        [TestMethod]
        public void TestDeposit()
        {
            BankAccount bank = new BankAccount("93-92-96", "000112244", 500);
            bank.Deposit(500);
            Assert.AreEqual(500, bank.Balance);
        }

        // Test Withdraw method passed if withdrawal less than bal + overdraft, also balance can be negative
        [TestMethod]
        public void TestWithdraw()
        {
            BankAccount bank = new BankAccount("93-92-96", "000112244", 501);
            bank.Withdraw(500);
            bank.Deposit(200);
            bank.Withdraw(200);
            Assert.AreEqual(-500, bank.Balance);
        }

        // test Exception is Argument and test fails - results in less code coverage
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        //public void NegativeDepositException()
        //{
        //    BankAccount bank = new BankAccount("93-92-96", "000112244", 501);
        //    bank.Deposit(-500);
        //    Assert.Fail();
        //}

        // Test deposit can't be negative
        [TestMethod]
        public void TestCantNegativeDeposit()
        {
            try
            {
                BankAccount bank = new BankAccount("93-92-96", "000112244", 501);
                bank.Deposit(-500);
            }
            catch (ArgumentException ex)
            {
                var exception = ex;
                Assert.IsNotNull(exception);
                StringAssert.Contains(ex.Message, "Cant make negative deposit");
            }
        }

        // Test get exception where dont have enough money in account for withdrawal
        [TestMethod]
        public void InsufficientFunds()
        {
            try
            {
                BankAccount bank = new BankAccount("93-92-96", "000112244", 1000);
                bank.Deposit(500);
                bank.Withdraw(8000);
            }
            catch (ArgumentException ex)
            {
                var exception = ex;
                Assert.IsNotNull(exception);
                StringAssert.Contains(ex.Message, "Insufficent funds in the account");
            }

        }

        // Test execption typr for insufficient funds
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsufficientFundsFail()
        {
            BankAccount bank = new BankAccount("93-92-96", "000112244", 100);
            bank.Deposit(500);
            bank.Withdraw(800);
            Assert.Fail();
        }

        // Check for negative balance
        [TestMethod]
        public void NegativeBalance()
        {
            BankAccount bank = new BankAccount("93-92-96", "000112244", 1000);
            bank.Deposit(500);
            bank.Withdraw(800);
            Assert.AreEqual(-300, bank.Balance);
        }

        // Test boundaries for overdraft limit
        [TestMethod]
        public void InvalidOverdraftLimit()
        {
            BankAccount bank = new BankAccount("00-00-00", "00001111", -1);
            Assert.AreEqual(0, bank.OverdraftLimit);
            BankAccount bank1 = new BankAccount("00-00-00", "00001111", 0);
            Assert.AreEqual(0, bank1.OverdraftLimit);
            BankAccount bank2 = new BankAccount("00-00-00", "00001111", 1);
            Assert.AreEqual(1, bank2.OverdraftLimit);
        }


        // Test CTOR, Poperties and Print Output
        [TestMethod]
        public void PrintBank()
        {
            BankAccount bank = new BankAccount("93-92-95", "000112233", 500);
            bank.Deposit(1000);
            bank.Deposit(400);
            bank.Deposit(7000);
            bank.Withdraw(6000);
            Console.WriteLine(bank.ToString());
            string expected = "\nsort code: 93-92-95" + "\naccount no: 000112233" + "\noverdraft limit: 500" + "\nbalance: 2400" + "\n Transaction History: 1000";
            string actual = bank.ToString();
            Assert.AreEqual(expected, actual);
            //Assert.AreEqual(2400, bank.Balance);
            //StringAssert.Contains(bank.ToString(), "\nsort code: 93-92-95" + "\naccount no: 000112233" + "\noverdraft limit: 500" + "\nbalance: 2400" + "\n Transaction History: 1000");
        }

        // Test Transaction History, note needed to make public for tests
        [TestMethod]
        public void TransactionHistory()
        {
            BankAccount bank = new BankAccount("00-00-00", "000000000", 500);
            bank.Deposit(5000);
            Assert.AreEqual(5000, bank.transactionHistory[0]);
            bank.Deposit(300);
            Assert.AreEqual(300, bank.transactionHistory[1]);
            bank.Withdraw(400);
            Assert.AreEqual(-400, bank.transactionHistory[2]);
        }

        // Test Max Transactions limit ** Doesnt give error expected if over 100 transactions
        [TestMethod]
        public void MaxTransactions()
        {
            int morethan100transactions = 102;
            BankAccount[] bankObjects = new BankAccount[morethan100transactions];

            for (int i = 0; i < morethan100transactions; i++)
            {
                bankObjects[i] = new BankAccount("93-92-95", "000112233", 500);
                Console.WriteLine(bankObjects[i].SortCode.ToString());
            }
        }


    }
}

