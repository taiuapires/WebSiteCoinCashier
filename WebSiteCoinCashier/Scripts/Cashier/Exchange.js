$(document).ready(function () {
    
    $(document).on("click", "#confirmExchange", function (e) {
        e.preventDefault();

        var form = {
            totalChange: $("#changeRequired").val()
        };

        $.ajax({
            cache: false,
            type: "POST",
            url: "/Cashier/ExchangeFunds",
            data: form,
            success: function (result) {
                if (result.resultCode == 0) {
                    alert("Transaction Completed!");
                }
                else if (result.resultCode == 1) {
                    alert("Can't make change!");
                }
            },
            error: function (error) {

            }
        });
    });

});