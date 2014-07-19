/**
 * 事件系统与全局事件
 * 最后修改 张实唯2014.7.8
*/

/**
 * 事件系统
 * 这个存在的原因是为了跨浏览器，因为各个浏览器自定义事件的名称用法各不相同，使用相当麻烦。
*/

//保存各事件的函数
var _events = {};

//检查是不是指定了重复次数
var testreg = /^[\d]*\|/;

//event接受一个字符串和一个方法。如果方法存在则将方法注册到事件，如果不存在则触发该事件。
//字符串以'-'开头表示取消该事件中绑定的函数f，若不指定f则全部取消
//字符串以数字+'|'开头表示该函数只执行多少次，之后自动解除绑定，若省略数字则默认为1
//发布事件时可带一参数(参数不能是函数，但可以用包含一个函数的对象来作为参数)。绑定的函数可以直接接受该参数。(不明白的话直接来问我)
//在一些情况下可能会删除一些事件，比如页面切换等。如果希望某个事件只能被指定删除而不受"删除全部"的影响，可以在字符串开头加'_'
var event = function(s,f){
	if(typeof(s) == 'string' && s){
		if(s[0] == '-'){
			s=s.slice(1);
			if(s){
				if(typeof(f) == 'function'){
					if(!_events[s]){_events[s] = [];}
					if(!_events["_" + s]){_events["_" + s] = [];}
					event.unbind(s,f);
					event.unbind("_"+s,f);
				}else if(typeof(f) == 'undefined'){
					event.unbindall(s);
				}
			}
			return;
		}else if(testreg.test(s)){
			a = s.split("|");
			if(a[1]){
				if(!_events[a[1]]){_events[a[1]] = [];}
				if(typeof(f) == 'function'){
					if(!a[0]){a[0] = '1';}
					return event.bind(a[1],f,parseInt(a[0]));
				}
			}
			return;
		}
		if(!_events[s]){_events[s] = [];}
		if(typeof(f) == 'function'){
			return event.bind(s,f,-1);
		} else {
			if(!_events["_" + s]){_events["_" + s] = [];}
			event.trigger(s,f);
			event.trigger("_"+s,f);
		}
	}
};
//以下函数请勿直接调用
event.bind = function(s,f,t){
	_events[s].push({f:f,t:t});
	return f;
};
event.trigger = function(s,a){
	l = _events[s].length;
	for(i=0;i<l;i++){
		if(_events[s][i]){
			_events[s][i].f(a,_events[s][i].t-1);
			_events[s][i].t--;
			if(_events[s][i].t == 0)
			{
				if(event.unbind(s,_events[s][i].f))
				{
					i--;
					l--;
				}
			}
		}
	}
};
event.unbind = function(s,f){
	if(_events[s].length == 0){delete _events[s];return false;}
	if(_events[s][0].f == f){
		_events[s].shift();
		return true;
	}
	l = _events[s].length;
	for(i=1;i<l;i++){
		if(_events[s][i].f == f){
			_events[s].splice(i,i);
			return true;
		}
	}
	return false;
};
event.unbindall = function(s){
	delete _events[s];
};

/**
 * 全局事件，包括
 * onload : 加载完毕时触发的事件
*/
//加载完毕触发
window.onload = function(){
	event("onload");
	//event("-onload");
};
