// *
// Во-первых в XAML файле на (textbox/passwordbox) нужно задать свойство [keydown] и создать новое событие
// *
// Во-вторых в cs файле на созданное событие нужно добать код, указано в самом событий Example_KeyDown
// *


// *
// Обратим внимание на то, что в этом коде выполняется событие входа в личный кабинет
// и в нем есть функция authorization(), которая выполняет все другие события
// *
// Лирическое отступление, этот код служит для входа только при нажатий на кнопку через мышку
// *

private void Example_btn_Click(object sender, RoutedEventArgs e)
{
	authorization();
}


// *
// Данный код является функцией которая вызывается в Example_btn_click и Example_KeyDown
// в нем находится весь обрабатываемый код
// *

private void authorization()
{
	JArray base_request = Utils.GetBaseRequest(main_window);
	base_request.Add(new JObject());

	JObject data1 = base_request[1] as JObject;
	data1.Add(new JProperty("get", "user_auth"));
	data1.Add(new JProperty("email", Utils.Base64Encode(pop_input_login.Text)));
	data1.Add(new JProperty("pass", Utils.Base64Encode(pop_input_password.Password)));

	var client = new RestClient(main_window.BASE_URL);
	var request = new RestRequest("", Method.POST);
	request.AddParameter("request", base_request.ToString());

	ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;

	client.ExecuteAsync(request, response => {

		Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate
		{
			string cont = response.Content;
			JArray array = JArray.Parse(cont);

			string Answer = array[1]["answer"].Value<string>();

			if (Answer == "ok")
			{
				main_window.TOKEN = array[0]["token"].Value<string>();
			}
			else
			{
				MessageBox.Show(main_window.CURRENT_LANG.GetValue("wrong_data").ToString());
				Thread.Sleep(500);
				main_window.Back_Level.Children.Clear();
				AuthorizationWindow auth = new AuthorizationWindow(main_window);
				auth.Content = null;
				main_window.Middle_Level.Children.Add(auth.auth_window);
			}

			}));
		});

	popup_token.Visibility = Visibility.Collapsed;
}



// *
// А именно в этом коде события происходит вход через кнопку Enter
// *

private void Example_KeyDown(object sender, KeyEventArgs e)
{
	if (e.Key == Key.Enter)
	{
		authorization();
	}
}

