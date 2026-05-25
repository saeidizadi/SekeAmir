const { default: ur } = require("../../Common/ckeditor5/translations/ur");



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
        data: { ProductId: productId },
        success: function (result) {
            // نتیجه مستقیماً کدهای HTML تولید شده توسط PartialView است
            $("#priceTableBody").html(result);
        }
    });
}