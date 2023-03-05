using System.Runtime.InteropServices;

namespace ConsoleApplication3delegate
{
    class DepositCard
    {
        public int amount;

        public void Display()
        {
            Console.WriteLine("储蓄卡余额为：{0}", amount);
        }
        public void Account(int balance, int payday)
        {
            amount += balance;
            Console.WriteLine("今天是本月的{0}号，取款{1}，储蓄卡余额为：{2}。",DateTime.Today.Day, balance, amount);
        }
    }

    class CreditCard
    {
        private int billamount;
        private int repaymentday;

        public CreditCard(int billamount, int repaymentday)
        {
            this.billamount = billamount;
            this.repaymentday = repaymentday;
        }

        public int getbillamount() { return billamount; }

        public int getrepaymentday() { return repaymentday; }

        public void Display() { Console.WriteLine("信用卡余额为：{0}", billamount); }

        public void havePayed()
        {
            billamount = 0;
        }
    }

    class CreditCardDelegate
    {
        public int billamount;
        public int repaymentday;
        //请在此处添加自己的代码
        public CreditCardDelegate(int billamount, int repaymentday)
        {
            this.billamount = billamount;
            this.repaymentday = repaymentday;
        }
        public delegate void cardDelegate(int billmount,int repaymentday);
        public event cardDelegate notifyRepayment = delegate { };
        public void notify()
        {
            if(notifyRepayment != null)
            {
                notifyRepayment(billamount,repaymentday);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            DepositCard depositCard = new DepositCard();
            depositCard.amount = 10000;
            DepositCard depositCard1 = new DepositCard();
            depositCard1.amount = 2000;
            List<DepositCard> depositCards = new List<DepositCard>();
            depositCards.Add(depositCard); depositCards.Add(depositCard1);
            CreditCard creditCard1 = new CreditCard(-3000, 5);
            CreditCard creditCard2 = new CreditCard(-3000, 13);
            CreditCard creditCard3 = new CreditCard(-5000, 29);
            depositCard.Display(); 
            depositCard1.Display(); Console.WriteLine("");
            List<CreditCard> Cards = new List<CreditCard>();
            Cards.Add(creditCard1); Cards.Add(creditCard2); Cards.Add(creditCard3);

            Console.WriteLine("在您的账户下找到了{0}张储蓄卡，{1}张信用卡", depositCards.Count, Cards.Count);
            foreach (CreditCard card in Cards)
            {
                CreditCardDelegate creditCardDelegate = new CreditCardDelegate(card.getbillamount(), card.getrepaymentday());
                Console.WriteLine("");
                Console.WriteLine("信用卡开始执行委托还款。。。。。。");
                //请在此处添加自己的代码
                if (DateTime.Today.Day == card.getrepaymentday())
                {
                    Console.WriteLine("此张信用卡今天已需还款，还款金额为{0}，请按‘1’或者‘2’在您的两张储蓄卡中选择一张卡还款", card.getbillamount());
                    try
                    {
                        int number = Convert.ToInt32(Console.ReadLine());
                        if (number == 1)
                        {
                            if (depositCard.amount + card.getbillamount() >= 0)
                            {
                                creditCardDelegate.notifyRepayment += new CreditCardDelegate.cardDelegate(depositCard.Account);
                                creditCardDelegate.notify();
                                card.havePayed();
                                Console.WriteLine("付款成功，欢迎下次光临~");
                            }
                            else
                            {
                                Console.WriteLine("这张卡钱不够您还款,你怎么这么能花，自动选择另外一张卡为您还款");
                                if (depositCard1.amount + card.getbillamount() >= 0)
                                {
                                    creditCardDelegate.notifyRepayment += new CreditCardDelegate.cardDelegate(depositCard1.Account);
                                    creditCardDelegate.notify();
                                    card.havePayed();
                                    Console.WriteLine("付款成功，欢迎下次光临~");
                                }
                                else
                                {
                                    Console.WriteLine("这张卡钱也不够您还款，您的信息已被银行记录");
                                }
                            }
                        }
                        else if (number == 2)
                        {
                            if (depositCard1.amount + card.getbillamount() >= 0)
                            {
                                creditCardDelegate.notifyRepayment += new CreditCardDelegate.cardDelegate(depositCard1.Account);
                                creditCardDelegate.notify();
                                card.havePayed();
                                Console.WriteLine("付款成功，欢迎下次光临~");
                            }
                            else
                            {
                                Console.WriteLine("这张卡钱不够您还款,你怎么这么能花，自动选择另外一张卡为您还款");
                                if (depositCard.amount + card.getbillamount() >= 0)
                                {
                                    creditCardDelegate.notifyRepayment += new CreditCardDelegate.cardDelegate(depositCard.Account);
                                    creditCardDelegate.notify();
                                    card.havePayed();
                                    Console.WriteLine("付款成功，欢迎下次光临~");
                                }
                                else
                                {
                                    Console.WriteLine("这张卡钱也不够您还款，您的信息已被银行记录");
                                }
                            }
                        }
                    }
                    catch (Exception e) { Console.WriteLine(e + "输入错误，请配合程序工作，不要瞎输"); }
                }
                else
                {
                    Console.WriteLine("yes!，此卡未到您的还款日，下面为您显示此卡账单:");
                    card.Display();
                }
            }
            Console.WriteLine("");
            Console.WriteLine("信用卡还款流程结束，下面是您的{0}张卡现在余额,祝您生活愉快~", depositCards.Count);
            depositCard.Display();
            depositCard1.Display(); Console.WriteLine("");
        }
    }
}
