'use strict';

class Loader {

    /**
     * Callback that receives loaded data.
     * 
     * @callback loadCallback
     * @param {string} data New sensor data received from the server.
     */

    /**
     * Represents a dataentry that can be loaded regularely.
     * 
     * @param {string} value Sensor data to be loaded.
     * @param {loadCallback} callback Callback that is called when the data is loaded.
     */
    constructor(value, callback) {
        this.value = value;
        this.callback = callback;
    }
}

$(document).ready(function () {

    const loaders = [];

    const load = () => {
        // TODO: Implement
        loaders.forEach(loader => loader.callback('test'));
    };

    $('[data-mighty-load]').each(function () {
        const element = $(this);
        loaders.push(new Loader(element.attr('data-mighty-load'), value =>
            element.text(value)));
    });

    load();
});