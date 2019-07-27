$(document).ready(function () {

    $(document).on("click", "#resetCashier", function (e) {
        e.preventDefault();
        
        $.ajax({
            cache: false,
            type: "POST",
            url: "/Cashier/ResetCashier",
            data: null,
            success: function (result) {
                if (result.resultCode == 0) {
                    alert("Reset Complete!");
                    window.location.replace("/Cashier/Balance");
                }
            },
            error: function (error) {

            }
        });
    });

});