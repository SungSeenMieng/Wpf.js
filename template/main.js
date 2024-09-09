import {ViewModelData} from "ViewModelData.js"
var vmData = new ViewModelData("点击",0);
var vm = wpf.createViewModel(vmData);
var view = new Window();
view.DataContext = vm;
var  btn = new Button();
btn.Width = 120;
btn.Height = 40;
view.Content = btn;
wpf.setBinding(btn,"Content","Content");
wpf.setBinding(btn,"Command","ClickCommand");
view.ShowDialog();