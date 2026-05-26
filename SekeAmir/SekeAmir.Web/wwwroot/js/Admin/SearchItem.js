


function GetProductPriceByProductId(element) {
    var productId = $(element).val();
    var url = $(element).data('url');
    if (!productId) {
        $("#priceTableBody").html("");
        return;
    }

    $.ajax({
        url: url, // نام کنترلر و اکشن را چک کنید
        type: 'GET',
        data: { ProductId: productId, pageId: 1 },
        success: function (result) {
            // نتیجه مستقیماً کدهای HTML تولید شده توسط PartialView است
            $("#PriceTableBody").html(result);
        }
    });
}