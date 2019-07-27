$(document).ready(function () {
    
    $(document).on("click", "#confirmWithdraw", function (e) {
        e.preventDefault();

        var form = {
            coinValue: $("#coinValue").val(),
            quantity: $("#quantity").val()
        };

        $.ajax({
            cache: false,
            type: "POST",
            url: "/Cashier/WithdrawFunds",
            data: form,
            success: function (result) {
                if (result.resultCode == 0) {
                    alert("Funds Withdrawn!");
                }
                else if (result.resultCode == 1) {
                    alert("Invalid Coin Value!");
                }
                else if (result.resultCode == 2) {
                    alert("Invalid Quantity!");
                }
            },
            error: function (error) {

            }
        });
    });

});