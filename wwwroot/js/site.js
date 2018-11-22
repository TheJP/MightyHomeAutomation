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
     * @param {boolean|number} refresh Specifies, if the loader should be refreshed regularly and if yes, at what ms rate.
     */
    constructor(value, callback, refresh) {
        this.value = value;
        this.callback = callback;
        this.refresh = refresh;
        this.lastUpdate = new Date(0); // Never been updated so far
    }

    /**
     * Check if the value of this loader is outdated compared to the given time.
     * @param {Date} now Date used to check if the loader is outdated.
     */
    isOutdated(now) {
        if (!this.refresh) { return false; }
        else { return now - this.lastUpdate >= this.refresh; }
    }
}

$(document).ready(function () {

    /** @type {Loader[]} */
    const loaders = [];

    /** Load data for all loaders or for all loaders that satisfy the given filter. */
    const load = (filter = false) => {
        const filteredLoaders = filter ? loaders.filter(filter) : loaders;
        if (filteredLoaders.length <= 0) { return; }

        const queries = filteredLoaders.map(loader => loader.value);
        $.ajax({
            method: 'POST',
            url: '/api/devices/readsensors',
            data: JSON.stringify(queries),
            contentType: "application/json; charset=utf-8",
            dataType: 'json'
        })
            .done(data => {
                // Set new values at all targets
                const updated = Date.now();
                for (let i = 0; i < data.length; ++i) {
                    loaders[i].callback(data[i]);
                    loaders[i].lastUpdate = updated;
                }
            })
            .fail(error => console.error(error));
    };

    /** Load data for outdated loaders. */
    const loadOutdated = () => {
        const now = Date.now();
        load(loader => loader.isOutdated(now));
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
        const refreshRate = element.is('[data-mighty-refresh]') ?
            parseInt(element.attr('data-mighty-refresh')) : false;
        loaders.push(new Loader(element.attr('data-mighty-load'),
            value => element.text(value), refreshRate));
    });

    $('[data-mighty-button]').click(function () {
        const action = $(this).attr('data-mighty-button').split('.');
        executeAction(action[0], action[1]);
    });

    load();
    setInterval(loadOutdated, 1000);
});