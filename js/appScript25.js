var fromType;
var fromId;
var ToId;
var ToType;
var backlogId;

$(document).ready(function () {

    $(".chartsSprintDrop").click(function () {

        var sprintId = $(this).data('value');

        $.ajax({
            url: "/BurnDown/chartForSprint",
            data: { id: sprintId
            },
            dataType: 'html',
            cache: false,
            success: function (response) {

                //  ----------
                var textData = response.split("\n");
                var dIdeal = (JSON.parse(textData[0])).map(function (item) {
                    return [item.Timestamp, item.TaskEffortLeft];
                });
                var dActual = (JSON.parse(textData[1])).map(function (item) {
                    return [item.Timestamp, item.TaskEffortLeft];
                });

                $.plot($("#placeholder"),
       [{ label: "ideal burndown", data: dIdeal, lines: { show: true }, points: { show: true} }, { label: "remaining effort", data: dActual, lines: { show: true }, points: { show: true}}],
        { crosshair: { mode: "x" },
            grid: { hoverable: true, autoHighlight: false }, xaxis: { mode: "time", timeformat: "%y/%m/%d" }
        }

        );

                // --------
            }
        });
        
        $("#burndownSprint").html("Sprint : " + sprintId);
    });
   
    $('#overlay-loading, #overlay')
            .ajaxStart(function () {
                $(this).fadeIn("slow");
                //                $('.ajaxStatus').html("wait....");
            })
            .ajaxStop(function () {

                $(this).fadeOut("slow");
            });
    $(".datepicker").datepicker({ dateFormat: "dd.mm.yy" });
});

$(function () {


    $('#close-button').click(function () {

        $('#overlay-content, #overlay').hide();

    });
    dragableColumnStarter(".column");

});




function dragableColumnStarter(coulumnId) {
    $(coulumnId).sortable({
        connectWith: ".column",
        start: function (event, ui) {

            if (ui.item.parent().attr('class') == "column backlogColumn ui-sortable") {
                fromType = "backlog";
                fromId = 0;

            }
            else if
            (ui.item.parent().attr('class') == "column sprintBacklogColumn ui-sortable") {
                fromType = "sprint";
                fromId = ui.item.parent().data('value');

            }
        },

        stop: function (event, ui) {

            if (ui.item.parent().attr('class') == "column backlogColumn ui-sortable") {
                ToType = "backlog";
                ToId = 0;

            }
            else if
            (ui.item.parent().attr('class') == "column sprintBacklogColumn ui-sortable") {
                ToType = "sprint";
                ToId = ui.item.parent().data('value');

            }
            backlogId = ui.item.data('value');
            if (!(fromType == "backlog" && ToType == "backlog") || !(fromId == ToId)) {
                $.ajax({
                    url: "/BacklogItem/backlogItemToSprint",
                    data: { fromType: fromType,
                        fromId: fromId,
                        toType: ToType,
                        toId: ToId,
                        backlogId: backlogId
                    },
                    dataType: 'json',
                    cache: false,
                    success: function (responseText) {


                        if (responseText.message == "success") {

                            $.ajax({
                                url: "/BacklogItem/backlogItemsList",
                                data: { id: projectId
                                },
                                dataType: 'html',
                                cache: false,
                                success: function (response) {
                                    $('#backlogColumnDiv').html(response);
                                         }
                            });
                            $.ajax({
                                url: "/sprint/SprintsList",
                                data: { id: projectId
                                },
                                dataType: 'html',
                                cache: false,
                                success: function (response) {
                                    $('#sprintbacklogBody').html(response);
                                    dragableColumnStarter(".columnContainerBody .column");
                                              }
                            });
                            $('.ajaxStatus').html("Success..Oh Yea!!");

                        }
                        else if (responseText.message == "error") {

                            $('.ajaxStatus').html("Failed!!");
                        }
                    }
                });
            }
            else {
                $(ui.sender).sortable('cancel');

            }

        }

    });
    

    $(".column").disableSelection();

}


   