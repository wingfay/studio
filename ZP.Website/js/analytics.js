//------------------------------//
//　ログトラッキングメイン関数　//
//------------------------------//
function tracker(){

	//------------------//
	//　ページ情報取得　//
	//------------------//
	
	page_url=escape(location.href);
	referrer=escape(document.referrer);
	
	
	
	//--------------//
	//　訪問者特定　//
	//--------------//
	
	visitor_id=myGetCookie("visitor_id");
	if(visitor_id==null){
		var date = new Date();
		visitor_id=date.getTime()+Math.random(); //初訪問者にIDを生成し、クッキー書き込み●
		mySetCookie("visitor_id",visitor_id,365);
		new_visitor=1; //初訪問者フラグON●
	}else{
		visitor_id=myGetCookie("visitor_id"); //既訪問者のIDをクッキーより読み込み●
		new_visitor=0; //初訪問者フラグOFF●
	}
	
	
	
	//-----------------//
	//　ログPHPに送る　//
	//-----------------//

    var jsHost = (("https:" == document.location.protocol) ? "https://" : "http://");
    log_url = jsHost + server + '/log.php?site_id='+site_id+'&page_url='+page_url+'&referrer='+referrer+'&visitor_id='+visitor_id+'&new_visitor='+new_visitor+'&page_id='+page_id+'&page_flg='+page_flg+'&id='+id+'&sub_id='+sub_id+'&member_id='+member_id;

	document.write("<img src='"+log_url+"' border='0' style='border:0px;' />");
}



//------------//
//　一般関数　//
//------------//

/* クッキーに登録する汎用関数                     */
/* 書式 : mySetCookie(クッキー名,値,有効期限日数) */
/* 戻り値 : なし(void)                            */
function mySetCookie(myCookie,myValue,myDay){
	myExp = new Date();
	myExp.setTime(myExp.getTime()+(myDay*24*60*60*1000));
	myItem = "@" + myCookie + "=" + escape(myValue) + ";";
	myExpires = "expires="+myExp.toGMTString();
	document.cookie =  myItem + myExpires + "path=/";
}

/* クッキーを取り込む汎用関数                     */
/* 書式 : myGetCookie(クッキー名)                 */
/* 戻り値 : 値(string)  null:該当なし             */
function myGetCookie(myCookie){
	myCookie = "@" + myCookie + "=";
	myValue = null;
	myStr = document.cookie + ";" ;
	myOfst = myStr.indexOf(myCookie);
	if (myOfst != -1){
		myStart = myOfst + myCookie.length;
		myEnd   = myStr.indexOf(";" , myStart);
		myValue = unescape(myStr.substring(myStart,myEnd));
	}
	return myValue;
}

