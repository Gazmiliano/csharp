// *
// Эту часть кода нужно добавить в XAML файл
// *

xmlns:gif="http://wpfanimatedgif.codeplex.com"



// *
// Его составляющая в гриде с указанием пути к gif файлу
// *

<Grid
	x:Name="image_preloader"
	Background="#7AFFFFFF"
	Visibility="Collapsed">
	
	<Image
		x:Name="gif_preloader"
		Height="40"
		gif:ImageBehavior.AnimatedSource="..\Assets\preloader.gif"/>

</Grid>



// *
// Эту часть кода нужно добавить в cs файл, запускает анимацию
// *
// Для того чтобы откл.чить анимацию достаточно переписать на Visibility.Collapsed;
// *

image_preloader.Visibility = Visibility.Visible;
image_preloader.UpdateLayout();