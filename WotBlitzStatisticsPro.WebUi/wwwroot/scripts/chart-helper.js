var chartsHelper = chartsHelper || {};

chartsHelper.BuildHistoryChart = function (elementId, label, data) {
  const ctx = document.getElementById(elementId).getContext('2d');
  const myChart = new Chart(ctx, {
    type: 'line',
    data: data,
    options: {
      maintainAspectRatio: false,
      responsive: true,
      plugins: {
        legend: {
          display: false,
        },
        title: {
          display: true,
          text: label,
        },
      },
    },
  });

  return myChart;
};

chartsHelper.addData = function (chart, newData) {
  chart.data = newData;
  chart.update();
};

chartsHelper.removeData = function (chart) {
  chart.data.labels = [];
  chart.data.datasets = [];
  chart.update();
};
