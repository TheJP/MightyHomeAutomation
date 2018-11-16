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

    /** @type {Loader[]} */
    const loaders = [];

    const load = () => {
        const queries = loaders.map(loader => loader.value);
        $.ajax({
            method: 'POST',
            url: '/api/devices/readsensors',
            data: JSON.stringify(queries),
            contentType: "application/json; charset=utf-8",
            dataType: 'json'
        })
            .done(data => {
                // Set new values at all targets
                for (let i = 0; i < data.length; ++i) {
                    loaders[i].callback(data[i]);
                }
            })
            .fail(error => console.error(error));
    };

    /**
     * Executes given action on device with given id remotely.
     * 
     * @param {string} deviceId Id of the device for which action should be executed.
     * @param {string} actionName Name of the action to execute.
     */
    function executeAction(deviceId, actionName) {
        $.ajax({
            method: 'POST',
            url: `/api/devices/${deviceId}/actions/${actionName}/execute`
        })
            .done(() => load())
            .fail(error => console.error(error));
    }

    $('[data-mighty-load]').each(function () {
        const element = $(this);
        loaders.push(new Loader(element.attr('data-mighty-load'),
            value => element.text(value)));
    });

    $('[data-mighty-button]').click(function () {
        const action = $(this).attr('data-mighty-button').split('.');
        executeAction(action[0], action[1]);
    });

    load();
});