function formatNumber(number, pattern) {
    var str = number.toString();
    var strInt;
    var strFloat;
    var formatInt;
    var formatFloat;
    if (/\./g.test(pattern)) {
        formatInt = pattern.split('.')[0];
        formatFloat = pattern.split('.')[1];
    } else {
        formatInt = pattern;
        formatFloat = null;
    }

    if (/\./g.test(str)) {
        if (formatFloat != null) {
            var tempFloat = Math.round(parseFloat('0.' + str.split('.')[1]) * Math.pow(10, formatFloat.length)) / Math.pow(10, formatFloat.length);
            strInt = (Math.floor(number) + Math.floor(tempFloat)).toString();
            strFloat = /\./g.test(tempFloat.toString()) ? tempFloat.toString().split('.')[1] : '0';
        } else {
            strInt = Math.round(number).toString();
            strFloat = '0';
        }
    } else {
        strInt = str;
        strFloat = '0';
    }
    if (formatInt != null) {
        var outputInt = '';
        var zero = formatInt.match(/0*$/)[0].length;
        var comma = null;
        if (/,/g.test(formatInt)) {
            comma = formatInt.match(/,[^,]*/)[0].length - 1;
        }
        var newReg = new RegExp('(\\d{' + comma + '})', 'g');

        if (strInt.length < zero) {
            outputInt = new Array(zero + 1).join('0') + strInt;
            outputInt = outputInt.substr(outputInt.length - zero, zero)
        } else {
            outputInt = strInt;
        }

        var 
        outputInt = outputInt.substr(0, outputInt.length % comma) + outputInt.substring(outputInt.length % comma).replace(newReg, (comma != null ? ',' : '') + '$1')
        outputInt = outputInt.replace(/^,/, '');

        strInt = outputInt;
    }

    if (formatFloat != null) {
        var outputFloat = '';
        var zero = formatFloat.match(/^0*/)[0].length;

        if (strFloat.length < zero) {
            outputFloat = strFloat + new Array(zero + 1).join('0');
            //outputFloat        = outputFloat.substring(0,formatFloat.length);
            var outputFloat1 = outputFloat.substring(0, zero);
            var outputFloat2 = outputFloat.substring(zero, formatFloat.length);
            outputFloat = outputFloat1 + outputFloat2.replace(/0*$/, '');
        } else {
            outputFloat = strFloat.substring(0, formatFloat.length);
        }

        strFloat = outputFloat;
    } else {
        if (pattern != '' || (pattern == '' && strFloat == '0')) {
            strFloat = '';
        }
    }

    return strInt + (strFloat == '' ? '' : '.' + strFloat);
}

//用正则, 且精确到秒
function TimeCompareMinsec(day1, day2) {
    var y1, y2, y3, m1, m2, m3, d1, d2, d3, h1, h2, h3, _m1, _m2, _m3, s1, s2, s3;
    var reg = /年|月|日 |\/|:| /;
    //dayinfo -  用正则分割
    var DI1 = day1.split(reg);
    var DI2 = day2.split(reg);

    var date1 = new Date(DI1[0], DI1[1], DI1[2], DI1[3], DI1[4], DI1[5]);
    var date2 = new Date(DI2[0], DI2[1], DI2[2], DI2[3], DI2[4], DI2[5]);

    //用距标准时间差来获取相距时间
    var minsec = Date.parse(date1) - Date.parse(date2);
    var days = minsec / 1000 / 60 / 60 / 24; //factor: second / minute / hour / day
    return minsec;
}
function TimeNowCompareMinsec(day1) {

    //用距标准时间差来获取相距时间
    return NewDate(day1) - Date.now();
}
function TimeCompareDays(day1, day2) {
    var y1, y2, y3, m1, m2, m3, d1, d2, d3, h1, h2, h3, _m1, _m2, _m3, s1, s2, s3;
    var reg = /年|月|日 |\/|:| /;
    //dayinfo -  用正则分割
    var DI1 = day1.split(reg);
    var DI2 = day2.split(reg);

    var date1 = new Date(DI1[0], DI1[1], DI1[2], DI1[3], DI1[4], DI1[5]);
    var date2 = new Date(DI2[0], DI2[1], DI2[2], DI2[3], DI2[4], DI2[5]);

    //用距标准时间差来获取相距时间
    var minsec = Date.parse(date1) - Date.parse(date2);
    var days = minsec / 1000 / 60 / 60 / 24; //factor: second / minute / hour / day
    return days;
}
function TimeNowCompareDays(time) {
    //用距标准时间差来获取相距时间
    var minsec = time - Date.now();
    var days = minsec / 1000 / 60 / 60 / 24; //factor: second / minute / hour / day
    return days;
}
function TimeCompareDifference(day1, day2) {
    var minsec = TimeCompareMinsec(day1, day2);
    var minsectemp = minsec;
    var days = parseInt(minsectemp / 1000 / 60 / 60 / 24);  //factor: second / minute / hour / day
    minsectemp = minsectemp - days * 1000 * 60 * 60 * 24;
    var hours = parseInt(minsectemp / 1000 / 60 / 60); //factor: second / minute / hour
    minsectemp = minsectemp - hours * 1000 * 60 * 60;
    var minutes = parseInt(minsectemp / 1000 / 60); //factor: second / minute
    minsectemp = minsectemp - minutes * 1000 * 60;
    var seconds = parseInt(minsectemp / 1000); //factor: second 
    var _text = '';
    if (days > 0) {
        _text = _text + days + '天';
    }
    if (hours > 0 || minutes > 0 || seconds > 0) {
        _text = _text + hours + '时';
    }
    if (minutes > 0 || seconds > 0) {
        _text = _text + minutes + '分';
    }
    if (seconds > 0) {
        _text = _text + seconds + '秒';
    }
    return _text;
}
function NewDate(dayStr) {
    var reg = /-|- |\/|:| /;
    var str = dayStr.split(reg);

    var date = new Date();
//    date.setUTCFullYear(str[0], str[1] - 1, str[2]);
//    date.setUTCHours(str[3], str[4], str[5], 0);
    date.setFullYear(str[0], str[1] - 1, str[2]);
    date.setHours(str[3], str[4], str[5], 0);
    return date;
} 

//alert(formatNumber(0, ''));
//alert(formatNumber(12432.21, '#,###'));
//alert(formatNumber(12432.21, '#,###.000#'));
//alert(formatNumber(12432, '#,###.00'));
//alert(formatNumber('12432.415', '#,###.0#'));



function reload() { window.location.reload(); }
function Goto(url) { location.href = url; }
function TopGoto(url) { top.location.href = url; }
function NewGoto(url) { window.open(url); }