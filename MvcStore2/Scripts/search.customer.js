$(function () {
    $("#btnSearch").click(function (event) {
        event.preventDefault();
        var dni = $("#dni").val();
        if (dni.length == 8) {
            $.ajax({
                url: "/Customers/Search",
                type: "POST",
                data: { DNI: dni },
                datatype: "json",
                beforeSend: function () {
                    $(".ContentForm").fadeOut();
                    //$("#FullName").html("");
                    //$("#OwnerId").html("");
                    //$("#notFound").html("");
                    //$("#ajax-loader").show();
                },
                complete: function () {
                    //$("#ajax-loader").hide();
                },
                error: function () {
                    var html_links = $("#link").html();
                    $("#message").html(html_links);

                },
                success: function (data) {
                    if (data.message) {
                        $("#CUSTOMER_ID").val(data.id);
                        $("#message").html("CLIENTE : <b>" + data.name + "</b>");
                        $(".ContentForm").fadeIn();
                    } else {
                        var html_links = $("#link").html();
                        $("#message").html(html_links);
                    }
                }
            });
        }

    });

});