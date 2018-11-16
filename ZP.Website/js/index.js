$(document).ready(function() {

    var carouselNum1 =0;
    var carouselPanelNum1 = $('ul#JUCarouselControll1 li').length;

    function mycarousel_initCallback(carousel) {

        $('ul#JUCarouselControll1 li').click(function(){
            $('ul#JUCarouselControll1 li').each(function(){
                $(this).removeClass('ROn');
            });
            $(this).addClass('ROn');
            panelNum1 = parseInt($(this).text()-1)*4+1;
            carousel.scroll(jQuery.jcarousel.intval(panelNum1));
            if( parseInt($(this).text()) > carouselPanelNum1 ){
                carouselNum1 = 0;
            }
            else { carouselNum1=parseInt($(this).text())-1;}

        });

    };

    function func_onAfterAnimation(carousel) {

        if( carouselNum1 < carouselPanelNum1){
            carouselNum1++;
        }
        else { carouselNum1 = 1;}

        $('ul#JUCarouselControll1 li').each(function(){
            $(this).removeClass('ROn');
        });
        var objName = '.JLi'+carouselNum1;
        $(objName).addClass('ROn');
    };

    $('#JUCarousel1').jcarousel({
        auto: 4,
        scroll: 4,
        wrap:"last",
        initCallback: mycarousel_initCallback,
        itemLoadCallback: {
            onAfterAnimation: func_onAfterAnimation
        }
    });
    modalLoad(0, '#jsSeoModal');
});

//タブ切り替え
$(function(){
    $("a.CATabReco1").click(function () {
        $(".ROn").removeClass("ROn");
        $(this).addClass("ROn");
        $(".SDLM1RecoEvent1").css("display","none");
        var content_show = $(this).attr("id").replace("CA","");
        $("#"+content_show).css("display","block");
    });
    $("a.CATabReco2").click(function () {
        $("a.CATabReco2.ROn").removeClass("ROn");
        $(this).addClass("ROn");
        $(".SDLM1RecoEvent2").css("display","none");
        var content_show = $(this).attr("id").replace("CA","");
        $("#"+content_show).css("display","block");
    });
});
