google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(function () {
    charts.drawAllCharts();
});

var charts = {
    drawAllCharts() {
        this.drawInventoryByCategoryChart();
        this.drawRevenueByCategoryChart();
        this.drawRevenueByCustomerChart();
        this.drawRevenueByYearChart();
        this.drawRevenueByQuarterChart();
        this.drawRevenueByMonthChart();
    },
    drawInventoryByCategoryChart() {
        this.load("inventory-by-category", resp => {
            this["inventory-by-category"] = resp;
            var info = {
                title: "TỒN KHO THEO LOẠI",
                data: [['Category', 'Inventory']],
                type: "column"
            }
            info.data.push(...resp.map(g => [g.group.nameVN, g.sum]));
            this.draw(info, "#inventory-by-category");
        });
    },
    drawRevenueByCategoryChart() {
        this.load("revenue-by-category", resp => {
            this["revenue-by-category"] = resp;
            var info = {
                title: "DOANH THU THEO LOẠI",
                data: [['Category', 'Revenue']],
                type: "pie"
            }
            info.data.push(...resp.map(g => [g.group.nameVN, g.sum]));
            this.draw(info, "#revenue-by-category");
        });
    },
    drawRevenueByCustomerChart() {
        this.load("revenue-by-customer", resp => {
            this["revenue-by-customer"] = resp;
            var info = {
                title: "TOP 10 KHÁCH HÀNG VIP",
                data: [['Customer', 'Revenue']],
                type: "line"
            }
            info.data.push(...resp.map(g => [g.group.id, g.sum]));
            this.draw(info, "#revenue-by-customer");
        });
    },
    drawRevenueByYearChart() {
        this.load("revenue-by-year", resp => {
            this["revenue-by-year"] = resp;
            var info = {
                title: "DOANH THU THEO NĂM",
                data: [['Year', 'Revenue']],
                type: "column"
            }
            info.data.push(...resp.map(g => ["" + g.group, g.sum]));
            this.draw(info, "#revenue-by-year");
        });
    },
    drawRevenueByQuarterChart() {
        this.load("revenue-by-quarter", resp => {
            this["revenue-by-quarter"] = resp;
            var info = {
                title: "DOANH THU THEO QUÝ",
                data: [['Quarter', 'Revenue']],
                type: "pie"
            }
            info.data.push(...resp.map(g => ["Quarter " + g.group, g.sum]));
            this.draw(info, "#revenue-by-quarter");
        });
    },
    drawRevenueByMonthChart() {
        this.load("revenue-by-month", resp => {
            this["revenue-by-month"] = resp;
            var info = {
                title: "DOANH THU THEO THÁNG",
                data: [['Month', 'Revenue']],
                type: "line"
            }
            info.data.push(...resp.map(g => ["Month " + g.group, g.sum]));
            this.draw(info, "#revenue-by-month");
        });
    },
    draw(info, selector) {
        var chart, elem = $(selector).get(0);
        if (info.type.toLowerCase() == "line") {
            chart = new google.visualization.LineChart(elem);
        } else if (info.type.toLowerCase() == "column") {
            chart = new google.visualization.ColumnChart(elem);
        } else {
            chart = new google.visualization.PieChart(elem);
        }
        var dataTable = google.visualization.arrayToDataTable(info.data);
        chart.draw(dataTable, { title: info.title });
    },

    load(path, onload) {
        $.ajax({
            url: "/admin/report/" + path,
            success: function (resp) {
                onload(resp);
            }
        })
    },
    show_details(data) {
        var trs = [];
        data.forEach(item => {
            if (item.group.nameVN) {
                item.group = item.group.nameVN;
            }
            if (item.group.id) {
                item.group = item.group.id;
            }
            var tr = `<tr>
                <td>${item.group}</td>
                <td>$${Math.round(item.sum * 100) / 100}</td>
                <td>${item.count}</td>
                <td>$${Math.round(100 * item.min) / 100}</td>
                <td>$${Math.round(100 * item.max) / 100}</td>
                <td>$${Math.round(100 * item.avg) / 100}</td>
            </tr> `;
            trs.push(tr);
        })
        $("tbody.result").html(trs.join(''))
        console.log(data)
    }
}

$(function () {
    $(".chart-wrapper").click(function () {
        var id = $(this).find("div").attr("id");
        charts.show_details(charts[id])
    });

    $(window).resize(function () {
        charts.drawAllCharts();
    });

    $.ajax({
        url: "/admin/report/summary",
        success: function (resp) {
            for (var key in resp) {
                $(`.order${key}`).html(resp[key]);
            }
        }
    })
});