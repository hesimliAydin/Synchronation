using Synchronation.Domain.Commands;
using Synchronation.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Synchronation.Domain.ViewModels
{
    public class MainWindowViewModel:BaseViewModel
    {
		private int money;

		public int Money
		{
			get { return money; }
			set { money = value; OnPropertyChanged(); }


		}

		private bool isEnabled;

		public bool IsEnabled
		{
			get { return isEnabled; }
			set { isEnabled = value;OnPropertyChanged(); }
		}

		private string cardNumber;

		public string CardNumber
		{
			get { return cardNumber; }
			set { cardNumber = value; OnPropertyChanged(); }
		}


		private int progressValue;

		public int ProgressValue
		{
			get { return progressValue; }
			set { progressValue = value;OnPropertyChanged(); }
		}

		private double balance;

		public double Balance
		{
			get { return balance; }
			set { balance = value;OnPropertyChanged(); }
		}

		public List<User> Users { get; set; }


		private User user;

		public User User
		{
			get { return user; }
			set { user = value;OnPropertyChanged(); }
		}

		public int RequestCount { get; set; }


		public RelayCommand LoadDataCommand { get; set; }
		public RelayCommand InsertCardCommand { get; set; }
		public RelayCommand TransferMoneyCommand { get; set; }



		public MainWindowViewModel()
		{
			IsEnabled= false;
			User= new User();
			Users = new List<User>()
			{
				new User()
				{
                    CardNumber="123456789012",
					FullName="Aydin Hesimli",
					Balance=4321.43
                },
				new User()
				{
					CardNumber="112233445534",
					FullName="Kamran Kerimzade",
					Balance=2233.12

				},
				new User()
				{
					CardNumber="111222333456",
					FullName="Elvin Camalzade",
					Balance=4467.36
				}



			};

			InsertCardCommand = new RelayCommand((o) =>
			{
				IsEnabled = !IsEnabled;
			});


			LoadDataCommand = new RelayCommand((o) =>
			{
				User = Users.FirstOrDefault((u) => { return u.CardNumber == CardNumber; });
				Balance = User.Balance;
				if (User == null) MessageBox.Show("User not found");
			});

			TransferMoneyCommand = new RelayCommand((o) =>
			{
				Thread thread = new Thread(() =>
				{
					++RequestCount;

					lock(new object())
					{
						if (user.Balance > Money)
						{
							while (ProgressValue<100)
							{
								Balance -= Money / 10;
								OnPropertyChanged();
								ProgressValue += 10;
								Thread.Sleep(1000);
							}
						}
					}
				});
				thread.Start();
			});
		}
	}
}
