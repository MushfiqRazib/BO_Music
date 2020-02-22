// Read a page's GET URL variables and return them as an associative array.







var UtilityService = new function() {
    return {
        getUrlVars: function() {
        var vars = [], hash;
        var urlString = decodeURI (window.location.href);
            var hashes = urlString.slice(urlString.indexOf('?') + 1).split('&');

            if (urlString != hashes) {
                for (var i = 0; i < hashes.length; i++) {
                    hash = hashes[i].split('=');
                    vars.push(hash[0]);
                    vars[hash[0]] = hash[1];
                }
            }
            return vars;
        },

        getUrlString: function(elem) {

            var objArr = new Array();
            for (i = 0; i < elem.length; i++) {

                objArr.push(elem[i] + "=" + elem[elem[i]]);
            }

            return objArr.join("&");

        }
        ,

        GetNumericRawData: function(numericText, CulterInfo) {

            if (CulterInfo.DecimalSeparator == ".") {
                return parseFloat( String(numericText).replace(
							CulterInfo.GroupSeparator,
							CulterInfo.DecimalSeparator));
            } else {
                return parseFloat(String(numericText).replace(
							CulterInfo.DecimalSeparator, '.'));
            }

        },
        GetNumericTextData: function(numericText, CulterInfo) {

            if (CulterInfo.DecimalSeparator != ".") {
                return numericText.replace('.',
							CulterInfo.DecimalSeparator);
            }
            else {
                return numericText.replace(',', CulterInfo.DecimalSeparator);
            }
            return numericText;
        }
        
        
        
        
    }

}