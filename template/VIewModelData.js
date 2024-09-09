export class ViewModelData{
    constructor(content,count){
        this.set_Content(content);
        this.set_Count(count);
        var instance = this;
        this.click = function(){
            var count = instance.Count+1;
            instance.set_Count(count);
            instance.ClickCommand.NotifyCanExecuteChanged();
        };
        this.canClick = function(){
            if(instance.Count >10){
                return false;
            }
            return true;
        };
        this.ClickCommand = wpf.getCommand(this.click,this.canClick);
    }
}
ViewModelData.prototype.get_Content = function(){
    return this.Content;
}
ViewModelData.prototype.set_Content= function(content){
    this.Content = content;
    this.raisePropertyChanged("Content");
}
ViewModelData.prototype.get_Count = function(){
    return this.Count;
}
ViewModelData.prototype.set_Count = function(count){
    this.Count = count;
    if(this.Count>0){
        this.set_Content("count:"+this.Count);
    }
    this.raisePropertyChanged("Count");
}
ViewModelData.prototype.raisePropertyChanged = function(propertyName){}
