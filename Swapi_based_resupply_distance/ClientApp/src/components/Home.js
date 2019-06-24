import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/SwapiForecasts';

class Home extends Component {
  componentWillMount() {
    // This method runs when the component is first added to the page
    this.props.requestSwapiLoadData();
  }
  render() {
    return (
      <div>
        <h1>Welcome to my SWAPI Challenge!</h1>
        <br/>
        <h4>This is a simple front-end application (React + Redux) for displaying the results of the refueling calculations of all space ships of S.W. using the SWAPI API.</h4>
        <p>Welcome to your new single-page application, built with:</p>
        <ul>
          <li><a href='https://get.asp.net/' target="_blank">ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>
          <li><a href='https://redis.io/' target="_blank">Redis</a> in-memory data structure store, used as a database</li>
          <li><a href='https://facebook.github.io/react/' target="_blank">React</a> and <a href='https://redux.js.org/'>Redux</a> for client-side code</li>
          <li><a href='http://getbootstrap.com/' target="_blank">Bootstrap</a> for layout and styling</li>
          <li><a href='https://www.odata.org/' target="_blank">OData</a>, is one of the best practices for building and consuming RESTful API</li>
          <li><a href='https://swapi.co/' target="_blank">SWAPI</a> used to consume the data that will be handled in this application</li>
          <li><a href='https://www.docker.com/' target="_blank">Docker</a> used to build the cluster needed to connect and run the servers of all technologies used in a simple way is fast</li>
        </ul>
        <p>Some words:</p>
        <p>In order for the 'fetch data' tab to work perfectly, you need to access this page at least once, this is just to load the separate SWAPI data at a different time and to load using a developed API url, but remembering that it was only for option.</p>
        <br/>
        <p>You can also access the API directly by clicking <a href='http://10.1.0.5/api/starship' target="_blank">here</a> and using the filters in the OData standard to assemble the desired result.</p>
      </div>
    );
  }
}

export default connect(
  state => state.swapiForecasts,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Home);
