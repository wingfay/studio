/**
 */
(function($) {
    $(function() {
       var url = "../customerData/1.pdf";

        var pdfDoc = null,
            pageNum = 1,
            pageRendering = false,
            pageNumPending = null,
            scale = 1,
            canvas = document.getElementById("canvas"),
            ctx,
            $currentPageEle = $(".ppt-pager .current-page"),
            $totalPageEle = $(".ppt-pager .total-page"),
            $readPPT = $(".read-ppt"),
            resizeTimeout,
            isFirstRender = true;
        var $loadingEle = $(".loading");
        if (canvas) {
            ctx = canvas.getContext('2d');
        }

        function renderPage(num) {
            if (!isFirstRender) {
                $loadingEle.show();
            }
            pageRendering = true;
            pdfDoc.getPage(num).then(function(page) {
                var desireWidth = $readPPT.width() - 5,
                    viewport = page.getViewport(scale),
                    desireScale = desireWidth / viewport.width,
                    scaleViewPort = page.getViewport(desireScale);
                canvas.height = scaleViewPort.height;
                canvas.width = scaleViewPort.width;
                $readPPT.css("padding-bottom", canvas.height / canvas.width * $readPPT.width());
                var renderContext = {
                    canvasContext: ctx,
                    viewport: scaleViewPort
                };
                var renderTask = page.render(renderContext);
                renderTask.promise.then(function() {
                    pageRendering = false;
                    $(".ppt-pager").show();
                    $loadingEle.hide();
                    if (pageNumPending !== null) {
                        renderPage(pageNumPending);
                        pageNumPending = null;
                    }
                });
            });
            $currentPageEle.text(pageNum);
        }

        function queueRenderPage(num) {
            if (pageRendering) {
                pageNumPending = num;
            } else {
                renderPage(num);
            }
        }

        function onPrevPage() {
            if (pageNum <= 1) {
                return;
            }
            pageNum--;
            queueRenderPage(pageNum);
        }

        function onNextPage() {
            if (pageNum >= pdfDoc.numPages) {
                return;
            }
            pageNum++;
            queueRenderPage(pageNum);
        }

        if ($(".read-ppt canvas").length > 0) {
           $loadingEle.show();


            pdfjsLib.getDocument(url).then(function(pdfDoc_) {
                    pdfDoc = pdfDoc_;
                    $totalPageEle.text(pdfDoc.numPages);
                    renderPage(pageNum);
                    $('.read-ppt .prev').on('click', onPrevPage);
                    $('.read-ppt .next').on('click', onNextPage);
                    isFirstRender = false;
                },
                function error(e) {
                    alert(e.message);
                    $loadingEle.hide();
                });
            window.onresize = function() {
                if (pdfDoc) {
                    clearTimeout(resizeTimeout);
                    resizeTimeout = setTimeout(function() {
                            renderPage(pageNum);
                        },
                        200);
                }
            }
        }
    });
})(jQuery);
