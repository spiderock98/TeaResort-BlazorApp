function Inject_BootstrapMenu() {
  /////// Prevent closing from click inside dropdown
  document.querySelectorAll(".dropdown-menu").forEach(function (element) {
    element.addEventListener("click", function (e) {
      e.stopPropagation();
    });
  });

  // make it as accordion for smaller screens
  if (window.innerWidth < 992) {
    // close all inner dropdowns when parent is closed
    document
      .querySelectorAll(".navbar .dropdown")
      .forEach(function (everydropdown) {
        everydropdown.addEventListener("hidden.bs.dropdown", function () {
          // after dropdown is hidden, then find all submenus
          this.querySelectorAll(".submenu").forEach(function (everysubmenu) {
            // hide every submenu as well
            everysubmenu.style.display = "none";
          });
        });
      });

    document.querySelectorAll(".dropdown-menu a").forEach(function (element) {
      element.addEventListener("click", function (e) {
        let nextEl = this.nextElementSibling;
        if (nextEl && nextEl.classList.contains("submenu")) {
          // prevent opening link if link needs to open dropdown
          e.preventDefault();
          console.log(nextEl);
          if (nextEl.style.display == "block") {
            nextEl.style.display = "none";
          } else {
            nextEl.style.display = "block";
          }
        }
      });
    });
  }
  // end if innerWidth
}

function RenderPieChartLayout() {
  var donutData = {
    labels: ["Electrics", "HVAC"],
    datasets: [
      {
        data: [0, 55464],
        backgroundColor: [
          "#f56954",
          "#00a65a",
          "#f39c12",
          "#00c0ef",
          "#3c8dbc",
          "#d2d6de",
        ],
      },
    ],
  };

  //-------------
  //- PIE CHART -
  //-------------
  // Get context with jQuery - using jQuery's .get() method.
  var pieChartCanvas = $("#pieChart").get(0).getContext("2d");
  var pieData = donutData;
  var pieOptions = {
    maintainAspectRatio: false,
    responsive: true,
  };
  //Create pie or douhnut chart
  // You can switch between pie and douhnut using the method below.
  new Chart(pieChartCanvas, {
    type: "pie",
    data: pieData,
    options: pieOptions,
  });
}

function RenderAreaChartLayout(lstRangeDate) {
  /* ChartJS
   * -------
   * Here we will create a few charts using ChartJS
   */

  //--------------
  //- AREA CHART -
  //--------------

  const arrLabelDate = [];
  lstRangeDate.forEach((el) => {
    const _date = new Date(
      el.substr(0, 4),
      Number(el.substr(4, 2)) - 1,
      el.substr(6, 2)
    );
    arrLabelDate.push(`${_date.getDate()}/${_date.getMonth() + 1}`);
  });

  // Get context with jQuery - using jQuery's .get() method.
  var areaChartCanvas = $("#areaChart").get(0).getContext("2d");

  var areaChartData = {
    labels: arrLabelDate,
    datasets: [
      {
        label: "Daily Energy Consumption",
        backgroundColor: "rgba(60,141,188,0.9)",
        borderColor: "rgba(60,141,188,0.8)",
        pointRadius: true,
        pointColor: "#3b8bba",
        pointStrokeColor: "rgba(60,141,188,1)",
        pointHighlightFill: "#fff",
        pointHighlightStroke: "rgba(60,141,188,1)",
        data: [28, 48, 40, 19, 86, 27, 90],
      },
    ],
  };

  var areaChartOptions = {
    tooltips: {
      intersect: false,
    },
    maintainAspectRatio: false,
    responsive: true,
    legend: {
      display: true,
    },
    scales: {
      yAxes: [
        {
          gridLines: {
            display: true,
          },
          ticks: {
            beginAtZero: true,
          },
        },
      ],
    },
  };

  //   this will draw a vertial line on mouse hover
  Chart.defaults.LineWithLine = Chart.defaults.line;
  Chart.controllers.LineWithLine = Chart.controllers.line.extend({
    draw: function (ease) {
      Chart.controllers.line.prototype.draw.call(this, ease);

      if (this.chart.tooltip._active && this.chart.tooltip._active.length) {
        var activePoint = this.chart.tooltip._active[0],
          ctx = this.chart.ctx,
          x = activePoint.tooltipPosition().x,
          topY = this.chart.legend.bottom,
          bottomY = this.chart.chartArea.bottom;

        // draw line
        ctx.save();
        ctx.beginPath();
        ctx.moveTo(x, topY);
        ctx.lineTo(x, bottomY);
        ctx.lineWidth = 2;
        ctx.strokeStyle = "#07C";
        ctx.stroke();
        ctx.restore();
      }
    },
  });

  // This will get the first returned node in the jQuery collection.
  new Chart(areaChartCanvas, {
    // type: "line",
    type: "LineWithLine",
    data: areaChartData,
    options: areaChartOptions,
  });
}

//state: [show, hide]
function Interop_ShowHideModal(mdId, state) {
  $(`#${mdId}`).modal(`${state}`);
}

function Interop_GotoUrlAsync(uri, timeout) {
  setTimeout(() => {
    document.getElementById(uri).scrollIntoView({ behavior: "smooth" });
  }, timeout);
}

function _handleSelect() {
  return new Promise((resolve, reject) => {
    $(".selectpicker").one(
      "changed.bs.select",
      (e, clickedIndex, isSelected, previousValue) => {
        const slOptionText = $(e.currentTarget)
          .children("option")
          .eq(clickedIndex)
          .val();
        resolve([slOptionText, isSelected]);
      }
    );
  });
}

function _handlejQuerySelect() {
  return new Promise((resolve, reject) => {
    $("select").one("change", function (e) {
      var valueSelected = this.value;
      resolve(valueSelected);
    });
  });
}

async function Interop_jQueryDisplayChange() {
  let result = await _handlejQuerySelect();
  console.log(result);
  return result;
}

async function Interop_DisplayChange() {
  //console.log(el);
  let arrResult = await _handleSelect();
  //console.log($(el).val());
  console.log(arrResult);
  return arrResult;
}

function _handleSelectById(slElId) {
  return new Promise((resolve, reject) => {
    $(`#${slElId}`).one(
      "changed.bs.select",
      (e, clickedIndex, isSelected, previousValue) => {
        console.log(e);
        console.log("haha id", clickedIndex);
        const slOptionText = $(e.currentTarget)
          .children("option")
          .eq(clickedIndex)
          .val();
        resolve([slOptionText, isSelected]);
      }
    );
  });
}
async function Interop_DisplayChangeByJqueryId(slElId) {
  //let arrResult = await _handleSelectById(slElId);
  return $(`#${slElId}`).val();
}

function Interop_FormValidate(elForm) {
  elForm.classList.add("was-validated");
  return elForm.checkValidity();
}

// this is for event register and call this function once onAfterRender
function OneTimeEventHandle() {
  // $(".modal")
  //   .on("show.bs.modal", function (e) {
  //     $("body").addClass("example-open");
  //   })
  //   .on("hide.bs.modal", function (e) {
  //     $("body").removeClass("example-open");
  //   });
  // $(".modal").on("hide.bs.modal", function (e) {});
}

async function Interop_Swal2InputAsync(inputType, title, confirmText) {
  const result = await Swal.fire({
    title: title,
    input: inputType,
    showCancelButton: false,
    confirmButtonText: confirmText,
    inputValidator: (value) => {
      if (!value) {
        return "Please fill all required field!";
      }
    },
  });
  if (result.isConfirmed) {
    return result.value;
  }
  return "";
}

function OpenInNewTab(url) {
  console.log("haah");
  window.open(url, "_blank").focus();
}

async function Interop_Swal2HelperAsync(
  icon,
  text,
  confirmText,
  confirmColor,
  cancelText,
  isToast
) {
  const result = await Swal.fire({
    icon: `${icon}`,
    title: `${text}`,
    showCancelButton: true,
    confirmButtonText: confirmText,
    cancelButtonText: cancelText,
    confirmButtonColor: confirmColor,
  });
  if (result.isConfirmed) {
    return true;
  }
  return false;
}

function Interop_Swal2spinner(isSpin, title) {
  if (isSpin) {
    Swal.fire({
      title: title,
      showConfirmButton: false,
      allowOutsideClick: false,
      html: '<div class="spinner-grow text-info" style="width: 6rem; height: 6rem;" role="status"><span class="sr-only">Loading...</span></div>',
      onBeforeOpen: () => {
        Swal.showLoading();
      },
    });
  } else Swal.close();
}

function Interop_Swal2Helper(icon, text, isToast) {
  if (isToast) {
    var Toast = Swal.mixin({
      toast: true,
      position: "top-end",
      showConfirmButton: false,
      timer: 3000,
    });
    Toast.fire({
      icon: `${icon}`,
      title: `${text}`,
    });
  } else {
    Swal.fire({
      icon: `${icon}`,
      title: `${text}`,
    });
  }
}

function Interop_ToggleSwitch(cbId) {
  let cbEl = $(`#${cbId}`);
  cbEl.prop("checked", !cbEl.prop("checked"));
  console.log(cbId);
}

function Interop_InjectBootstrapTable() {
  const $table = $(".bootstrap-table");
  $table.bootstrapTable({
    exportDataType: "all",
    exportTypes: ["json", "xml", "csv", "txt", "sql", "excel", "pdf"],
    formatNoMatches: () => {
      return "";
    },
  });
}

function InjectDataTable() {
  $("#example1")
    .DataTable({
      responsive: true,
      lengthChange: false,
      autoWidth: false,
      buttons: ["copy", "csv", "excel", "pdf", "print", "colvis"],
    })
    .buttons()
    .container()
    .appendTo("#example1_wrapper .col-md-6:eq(0)");
}

// function InjectSelect2() {
//   $(".select2").select2();
// }

function Interop_InjectSummerNoteDefault(
  id,
  _height,
  _toggleCodeView,
  _toggleEditable
) {
  $(`#${id}`).summernote({
    height: _height,
    toolbar: false,
    spellCheck: false,
    disableGrammar: false,
  });
  if (_toggleEditable) {
    $(`#${id}`).summernote("editable.toggle");
  }
  if (_toggleCodeView) {
    $(`#${id}`).summernote("codeview.toggle");
  }
}
function Interop_SummerGetCode(id) {
  return $(`#${id}`).summernote("code");
}
function Interop_SummerSetCode(id, markupStr, _height) {
  $(`#${id}`)
    .summernote({
      height: _height,
    })
    .summernote("code", markupStr);
}

function Interop_PurgeValidBtCache() {
  $("form.was-validated").removeClass("was-validated");
}

function Interop_PurgeBootstrapSelectCache() {
  $(".selectpicker").selectpicker("render");
  $(".selectpicker").selectpicker("refresh");
}

function Interop_PurgeAllCache() {
  // clear all select bootstrap
  $(".selectpicker").selectpicker("deselectAll");
  $(".selectpicker").selectpicker("val", []);

  // clear all validate sign
  Interop_PurgeValidBtCache();
}

function Interop_InjectBootstrapSelectionById(id, value) {
  console.log(id, value);
  $(`#${id}`).selectpicker("val", value);
  Interop_PurgeBootstrapSelectCache();
}

function InjectBootstrapSwitch() {
  $("input[data-bootstrap-switch]").each(function () {
    $(this).bootstrapSwitch("state", $(this).prop("checked"));
  });
}

function InjectFormValidate() {
  // Fetch all the forms we want to apply custom Bootstrap validation styles to
  var forms = document.getElementsByClassName("needs-validation");
  // Loop over them and prevent submission
  var validation = Array.prototype.filter.call(forms, function (form) {
    form.addEventListener(
      "submit",
      function (event) {
        if (form.checkValidity() === false) {
          event.preventDefault();
          event.stopPropagation();
        }
        form.classList.add("was-validated");
      },
      false
    );
  });
}

//https://github.com/seiyria/bootstrap-slider
function InjectBootstrapSlider() {
  $(".slider").bootstrapSlider();
  $(".slider").bootstrapSlider("disable");
  // toggle slider state
  $("#cbEngergySlider").click(() => {
    let _isEnable = $("#cbEngergySlider").is(":checked");
    $(".slider").bootstrapSlider("toggle");

    if (_isEnable) {
      $(".card-header b").text("from 0 to 1000");
    } else {
      $(".card-header b").text("All");
    }
  });
  // on slider event
  $(".slider")
    .slider()
    .on("slideStop", () => {
      let slValue = $(".slider").bootstrapSlider("getValue");
      console.log(slValue);
      $(".card-header b").text(`from ${slValue[0]} to ${slValue[1]}`);
    });
}

function Interop_InjectBootstrapSelect() {
  console.log($(".selectpicker").val());
  $(".selectpicker").selectpicker();
  $(".btn-light").css("color", "#1f2d3d");
  $(".btn-light").css("border-color", "#d8d8d8");
}

$(document).ready(() => {});
