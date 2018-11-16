//指定されたURLへ遷移する、textがが入力されている場合、確認ダイアログを表示する
function movePage(url, text){

	if(text != '' && !confirm(text)){
		return;
	}else{
		window.location.href = url;
	}

}

// 全選択、全解除をチェックボックスで行う
function chkFind(form, chkId){
	var chkElement = $(chkId);
	var flg = 1;
	if (chkElement.checked == false) {
		flg = 0;
	}
	chkChgAll(form, flg);

}

// チェックボックスの全選択、全解除
function chkChgAll(form, flg, prefix){
	
	var e = document.forms[form];
	
	if (prefix == undefined){
    	 prefix = '';
	}
	
	for(i=0;i<e.length;i++){
		if (e[i].type == 'checkbox' && e[i].name.substring(0, prefix.length) == prefix) {	
			e[i].checked = flg;
		}
	}

}

// セレクトボックスの一括変更
function selectChgAll(form, val){

	var elements = document.forms[form];

	for(i=0;i<elements.length;i++){
		strtype = elements[i].type;
		if( strtype.indexOf("select") != -1){
			elements[i].value = val;
		}
	}

} 

// divで囲まれたスペースの表示・非表示を切り替え
function switchDisp(flg, id1, id2){
	
	//flgが0の場合はid1を表示、id2を非表示
	if(flg == '0'){
		if(id1 != ''){
			document.getElementById(id1).style.display = '';
		}
		if(id2 != ''){
			document.getElementById(id2).style.display = 'none';
		}
	}else{
		if(id1 != ''){
			document.getElementById(id1).style.display = 'none';
		}
		if(id2 != ''){
				document.getElementById(id2).style.display = '';
		}
	}

} 
// divで囲まれたスペースの表示・非表示を切り替え
function switchDisp2(id){
	
	if(document.getElementById(id).style.display != 'none'){
		document.getElementById(id).style.display = 'none';
	}else{
		document.getElementById(id).style.display = '';
	}

} 

// JavaScript Document
function initRollovers() {
	if (!document.getElementById) return
	
	var aPreLoad = new Array();
	var sTempSrc;
	var aImages = document.getElementsByTagName('img');

	for (var i = 0; i < aImages.length; i++) {		
		if (aImages[i].className == 'over') {
			var src = aImages[i].getAttribute('src');
			var ftype = src.substring(src.lastIndexOf('.'), src.length);
			var hsrc = src.replace(ftype, '_o'+ftype);

			aImages[i].setAttribute('hsrc', hsrc);
			
			aPreLoad[i] = new Image();
			aPreLoad[i].src = hsrc;
			
			aImages[i].onmouseover = function() {
				sTempSrc = this.getAttribute('src');
				this.setAttribute('src', this.getAttribute('hsrc'));
			}	
			
			aImages[i].onmouseout = function() {
				if (!sTempSrc) sTempSrc = this.getAttribute('src').replace('_o'+ftype, ftype);
				this.setAttribute('src', sTempSrc);
			}
		}
	}
}


function simple_tooltip(target_items, name){
 $(target_items).each(function(i){
		$("body").append("<div class='"+name+"' id='"+name+i+"'><p style='text-align:left;'>"+$(this).attr('title')+"</p></div>");
		//$("body").append("<div class='"+name+"_left' id='"+name+"'><p>"+$(this).attr('title')+"</p></div>");

		var my_tooltip = $("#"+name+i);
		
		if($(this).attr("title") != "" && $(this).attr("title") != "undefined" ){
		
		$(this).removeAttr("title").mouseover(function(){
					my_tooltip.css({display:"none"}).fadeIn(400);
		}).mousemove(function(kmouse){
				var border_top = $(window).scrollTop(); 
				var border_right = $(window).width();
				var left_pos;
				var top_pos;
				var Xoffset = 10;
				var Yoffset = 5;
				if(border_right - (Xoffset *2) >= my_tooltip.width() + kmouse.pageX){
					left_pos = kmouse.pageX+Xoffset;
					} else{
					left_pos = border_right-my_tooltip.width()-Xoffset;
					}
					
				if(border_top + (Yoffset *2)>= kmouse.pageY - my_tooltip.height()){
					top_pos = border_top +Yoffset;
					} else{
					top_pos = kmouse.pageY-my_tooltip.height()-Yoffset;
					}	
				
				
				my_tooltip.css({left:left_pos, top:top_pos});
		}).mouseout(function(){
				my_tooltip.css({left:"-9999px"});				  
		});
		
		}

	});
}

	
$(document).ready(function(){
	 simple_tooltip(".popup_text a","tooltip");
});



try{
	window.addEventListener("load",initRollovers,false);
}catch(e){
	window.attachEvent("onload",initRollovers);
}

//ドリルダウンセレクトボックス
function DrillDownSelect(selIdList){
	for(var i=0;selIdList[i];i++) {
		var DDS = new Object();
		var obj = document.getElementById(selIdList[i]);
		if(i){
			DDS.node=document.createElement('select');
			var GR = obj.getElementsByTagName('optgroup');
			while(GR[0]) {
				DDS.node.appendChild(GR[0].cloneNode(true));
				obj.removeChild(GR[0]);
			}
			obj.disabled = true;
		}
		if(selIdList[i+1]) {
			DDS.nextSelect = document.getElementById(selIdList[i+1]);
			obj.onchange = function(){DrillDownSelectEnabledSelect(this)};
		} else {
			DDS.nextSelect = false;
		}
		obj.DrillDownSelect = DDS;
	}
}

function DrillDownSelectEnabledSelect(oSel){
	var oVal = oSel.options[oSel.selectedIndex].value;
	if (oVal) {
		while (oSel.DrillDownSelect.nextSelect.options[1]) oSel.DrillDownSelect.nextSelect.remove(1);
		var eF = false;
		for(var OG=oSel.DrillDownSelect.nextSelect.DrillDownSelect.node.firstChild;OG;OG=OG.nextSibling) {
			if(OG.label == oVal) {
				eF = true;
				for(var OP=OG.firstChild;OP;OP=OP.nextSibling){
					oSel.DrillDownSelect.nextSelect.appendChild(OP.cloneNode(true));
				}
				break;
			}
		}
		oSel.DrillDownSelect.nextSelect.disabled = !eF;
	} else {
		oSel.DrillDownSelect.nextSelect.selectedIndex = 0;
		oSel.DrillDownSelect.nextSelect.disabled = true;
	}
	if (oSel.DrillDownSelect.nextSelect.onchange) oSel.DrillDownSelect.nextSelect.onchange();
}

function DefaultSelected(sSel, cId){
	var cSel = document.getElementById(sSel);
	for(i=0;i<cSel.options.length;i++){
		if(cSel.options[i].value == cId){
			cSel.options[i].selected = true;	
		}
	}
}

function getUniqStr(){
	var dt = new Date(); 
	var kazu = String(dt.getMinutes() + 10) + String(dt.getSeconds()) + String(dt.getTime());
	var teisu = 62;
	if (kazu == 0) {
		return '0';
	}
	var moji = '';
	ret = '';
	var nokori = kazu;
	var tmp = Math.floor(Math.log(nokori) / Math.log(teisu));
	var kurai = 0;
	for ( kurai = tmp; kurai >= 0; kurai--) {
		var keisu = Math.floor(nokori  / Math.pow(teisu, kurai));
		nokori = nokori - keisu * Math.pow(teisu, kurai);
		if (keisu < 10) {
			moji = keisu;
		} else if (keisu < 36) {
			moji = String.fromCharCode(65 + keisu -10);
		} else {
			moji = String.fromCharCode(97 + keisu -36);
		}
		ret = ret + moji;
	}
	return ret;
}

function swapimg(obj) {
	if(obj.src.match(/_f2.(jpg|gif|png)/)) {
		obj.src = obj.src.replace('_f2', '');
	} else {
		obj.src = obj.src.replace(/\.(jpg|gif|png)$/, "_f2.$1");
	}
}


$(document).ready(function() {

	$("a[href^=#]").click(function() { 
		var ScrollPosition = this.hash;
		if ($(ScrollPosition).size() > 0) {
			var ScrollAmount = $(ScrollPosition).position().top;
			$("html,body").stop().animate({
				scrollTop: ScrollAmount
			}, 1000);
			return false;
		}
	});


});

function show_content(id){
	document.getElementById(id).style.display = '';
}
function hide_content(id){
	document.getElementById(id).style.display = 'none';
}
