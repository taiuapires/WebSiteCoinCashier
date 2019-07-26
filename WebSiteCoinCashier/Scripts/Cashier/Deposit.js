$(document).ready(function () {
    alert("Deposit");

    $(document).on("click", "#confirmDeposit", function (e) {
        e.preventDefault();

        var form = {};

        $.ajax({
            cache: false,
            type: "POST",
            url: "/Cashier/AddCoins",
            data: form,
            success: function (retorno) {
                
            },
            error: function (e) {
                
            }
        });
    });
});