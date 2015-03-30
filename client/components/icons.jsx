/** @jsx React.DOM */

var React = require("react");
var _ = require('lodash');

var icons = {}

var icon = function(name, classes){
	var all = ['fa', 'fa-fw'].concat(classes);
	var clazz = _(all).join(' ');


	var component = React.createClass({
		displayName: name,
		render: function(){
			return (
				<i className={clazz}></i>
			);

		}
	});

	icons[name] = component;

	return component;
}


var Success = icon('success', ['fa-check', 'text-success']);
var Failed = icon('failed', ['fa-exclamation-circle', 'text-danger']);
var None = icon('none', ['fa-circle-o']);
var Running = icon('running', ['fa-spinner', 'fa-spin']);
var RunningSuccess = icon('running-success', ['fa-spinner', 'fa-spin', 'text-success']);
var RunningFailed = icon('running-failed', ['fa-spinner', 'fa-spin', 'text-danger']);
var Close = icon('close', ['fa-close']);

icon('remove', ['fa-remove']);
icon('folder-open', ['fa-caret-down']);
icon('folder-closed', ['fa-caret-right']);

icon('undo', ['fa-undo']);
icon('redo', ['fa-undo', 'fa-rotate-180']);
icon('run', ['fa-play']);
icon('save', ['fa-save']);
icon('copy', ['fa-copy']);
icon('stop', ['fa-stop']);


module.exports = icons;