(function ($) {
    "use strict";

    var current_fs, next_fs, previous_fs;
    var opacity;

    // 🔥 1. السحر هنا: بنعلم الـ jQuery إزاي يقرا الـ pattern أوتوماتيك من الـ HTML
    $.validator.addMethod("pattern", function (value, element, param) {
        if (this.optional(element)) {
            return true;
        }
        if (typeof param === "string") {
            param = new RegExp(param);
        }
        return param.test(value);
    }, "Format is invalid."); // الرسالة الافتراضية لو مفيش title

    // دالة إعدادات الفاليديشن
    var validationConfig = {
        errorClass: 'invalid',
        errorElement: 'span',
        errorPlacement: function (error, element) {
            var fieldName = element.attr("name");
            var errorSpan = $('[data-val-for="' + fieldName + '"]');

            if (errorSpan.length) {
                errorSpan.html('<i class="error-log fa fa-exclamation-triangle"></i> ').append(error);
            } else {
                error.insertAfter(element);
            }
        },
        highlight: function (element) {
            var fieldName = element.name;
            $('[data-val-for="' + fieldName + '"]').show();
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element) {
            var fieldName = element.name;
            $('[data-val-for="' + fieldName + '"]').hide();
            $(element).removeClass('is-invalid');
        }
    };

    // تشغيل الفاليديشن المبدئي
    $(".steps").validate(validationConfig);

    // 🔥 2. حركة صايعة: منع كتابة الحروف في وقتها الفعلي (Real-time)
    // الكود ده بيمسك أي إنبوت نوعه tel ويمنع الحروف تماماً
    $(document).on('keypress', 'input[type="tel"]', function (e) {
        var charCode = (e.which) ? e.which : e.keyCode;
        var charStr = String.fromCharCode(charCode);

        // بيسمح بس بـ: الأرقام، علامة +، المسافة، الشرطة -
        if (!/^[0-9+\s-]$/.test(charStr)) {
            e.preventDefault(); // ارفض الزرار وماتكتبش الحرف
        }
    });

    // كود زرار التالي (Next)
    $(".next").click(function () {
        $(".steps").validate(validationConfig);

        if ((!$('.steps').valid())) {
            return false;
        }

        current_fs = $(this).parent();
        next_fs = $(this).parent().next();
        $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");
        next_fs.show();
        current_fs.animate({
            opacity: 0
        }, {
            step: function (now) {
                opacity = 1 - now;
                next_fs.css({
                    'opacity': opacity
                });
            },
            duration: 1,
            complete: function () {
                current_fs.hide();
            }
        });
    });

    // كود زرار الـ Previous والـ Submit يكمل عادي زي ما هو...
    $(".previous").click(function () {
        current_fs = $(this).parent();
        previous_fs = $(this).parent().prev();
        $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");
        previous_fs.show();
        current_fs.animate({
            opacity: 0
        }, {
            step: function (now) {
                opacity = 1 - now;
                previous_fs.css({
                    'opacity': opacity
                });
            },
            duration: 1,
            complete: function () {
                current_fs.hide();
            }
        });
    });

}(jQuery));

