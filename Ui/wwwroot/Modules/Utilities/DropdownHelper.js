// ============================================================
//  DropdownHelper — generic select filler
// ============================================================
const DropdownHelper = {

    /**
     * Fill any <select> element from an array of items.
     *
     * @param {string}   selector        - CSS selector  e.g. '#countryId'
     * @param {Array}    items           - Array of objects from the API
     * @param {string}   valueField      - Which property becomes <option value="">
     * @param {string}   textField       - Which property becomes the visible label
     * @param {object}   [opts]
     * @param {string}   [opts.placeholder]  - First empty option e.g. '-- Select Country --'
     * @param {*}        [opts.selected]     - Pre-select this value after filling
    */

    fill(selector, items, valueField, textField, { placeholder, selected } = {}) {
        const $select = $(selector);

        if (!$select.length) {
            console.error(`[DropdownHelper] Element not found: "${selector}"`);
            return;
        }

        if (!Array.isArray(items) || !items.length) {
            console.warn(`[DropdownHelper] No items to fill into "${selector}"`);
            return;
        }

        // If textField is an object like { en: 'CountryEname', ar: 'CountryAname' }
        // pick the right field based on current lang
        if (typeof textField === 'object') {
            const lang = AppHelper.getCurrentLang();
            textField = textField[lang] || Object.values(textField)[0]; // fallback to first
        }

        // Resolve lang-aware placeholder
        if (typeof placeholder === 'object') {
            const lang = AppHelper.getCurrentLang();
            placeholder = placeholder[lang] || Object.values(placeholder)[0];
        }

        // Clear old options
        $select.empty();


        // Add placeholder option
        if (placeholder) {
            $select.append(
                $('<option>', { value: '', text: placeholder, disabled: true, selected: true })
            );
        }

        // Build all options in one go — faster than appending one by one
        const options = items.map(item =>
            $('<option>', { value: item[valueField], text: item[textField] })
        );
        $select.append(options);

        // Pre-select a value if given
        if (selected !== undefined && selected !== null) {
            $select.val(selected);
        }
    },

    /** Empty a dropdown and show a placeholder. */
    clear(selector, placeholder = '-- Select --') {
        // Resolve lang-aware placeholder
        if (typeof placeholder === 'object') {
            const lang = AppHelper.getCurrentLang();
            placeholder = placeholder[lang] || Object.values(placeholder)[0];
        }

        const $select = $(selector);
        $select.empty().append(
            $('<option>', { value: '', text: placeholder, disabled: true, selected: true })
        );
    },

    /** Disable or enable a dropdown. */
    setDisabled(selector, disabled = true) {
        $(selector).prop('disabled', disabled);
    },


    /**
     * Fill a dropdown with country options.     * @param {any} selector
     */
    fillCountryDropdown(selector) {
        CountryService.GetAll((response) => {
            DropdownHelper.fill(
                selector,
                response.Data,
                'Id',
                { en: 'CountryEname', ar: 'CountryAname' },
                { placeholder: { en: '-- Select Country --', ar: '-- اختر الدولة --' } }
            );
        }, (error) => {
            console.error('[DropdownHelper] Error fetching countries:', error);
        });
    },

    /**
     * fill a dropdown with shipping type options.
     * @param {any} selector
     */
    fillShippingTypesDropdown(selector) {
        ShippingTypeService.GetAll((response) => {
            DropdownHelper.fill(
                selector,
                response.Data,
                'Id',
                { en: 'ShippingTypeEname', ar: 'ShippingTypeAname' },
                { placeholder: { en: '-- Select Shipping Type --', ar: '-- اختر نوع الشحن --' } }
            );
        }, (error) => {
            console.error('[DropdownHelper] Error fetching shipping types:', error);
        });
    },

    /**
     * fill a dropdown with packaging type options.
     * @param {any} selector
     */
    fillPackgingDropdown(selector) {
        PackagingService.GetAll((response) => {
            DropdownHelper.fill(
                selector,
                response.Data,
                'Id',
                { en: 'PackagingEname', ar: 'PackagingAname' },
                { placeholder: { en: '-- Select Packaging --', ar: '-- اختر نوع التغليف --' } }
            );
        }, (error) => {
            console.error('[DropdownHelper] Error fetching packaging types:', error);
        });
    },

    /**
    * Fill a dropdown with city options based on selected country.
    * @param {string} cityselector    - e.g. '#cityId'
    * @param {string} countryselector - e.g. '#countryId'
    */
    fillCityDropdown(citySelector, countrySelector) {
        $(countrySelector).on('change', function () {
            const countryId = $(this).val();

            DropdownHelper.clear(citySelector, { en: '-- Loading... --', ar: '-- جاري التحميل... --' });
            DropdownHelper.setDisabled(citySelector, true);

            CityService.GetByCountryId(
                countryId,
                function (response) {
                    DropdownHelper.fill(
                        citySelector,
                        response.Data,
                        'Id',
                        { en: 'CityEname', ar: 'CityAname' },
                        { placeholder: { en: '-- Select City --', ar: '-- اختر المدينة --' } }
                    );
                    DropdownHelper.setDisabled(citySelector, false);
                },
                function (err) {
                    console.error('[DropdownHelper] Error fetching cities:', err);
                    DropdownHelper.clear(citySelector, { en: '-- Failed --', ar: '-- فشل التحميل --' });
                }
            );
        });
    },

};
