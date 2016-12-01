$(document).ready(function () {
    $('.orderAmountControl').change(function (e) {
        e.preventDefault();
        var dishID = $(this).closest('tr').find('.itemID').val();
        var amount = $(this).val();
        var unitPrice = $(this).siblings('.UnitPriceFor').val();
        var sumPrice = $(this).closest('tr').find('.PriceFor');

        $.ajax({
            url: updateURL,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                ID: dishID,
                Amount: amount
            }),
            success: function (data) {
                if (data.status != "Success") {
                    location.reload();
                }
                else {
                    sumPrice.html(data.PriceFor+' Ft');
                    $('#OrderSubmitButton').val('Megrendelés ('+ data.priceSum +' Ft)');
                }
            },
            error: function (data) {
                location.reload();
            }
        });

    });
});