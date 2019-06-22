import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../store/SwapiForecasts';

let distance = 0;

class FetchData extends Component {
  constructor(props)
    {
      super(props);
      this.state = {value: ''};
      this.handleClick = this.handleClick.bind(this);
      this.handleChange = this.handleChange.bind(this);
    }
  componentWillMount() {
    // This method runs when the component is first added to the page
    this.props.requestSwapiForecasts(distance);
  }

  handleChange = function(e) {
    this.state={value: e.target.value};;
  }

  handleClick = function(e) {
    console.log(this.state.value);
    if(this.state.value != null)
    {
      this.props.requestSwapiForecasts(this.state.value);
    }
  }
  
  render() {
    return (
      <div>
        <h1>Swapi forecast</h1>
        <p>This component demonstrates fetching data from the server and working with URL parameters.</p>
        <br/>
        Set the distance: <input type="number" id="distance" name="distance" onChange={this.handleChange}></input>
        <button onClick={this.handleClick}>Calculate</button>
        {renderForecastsTable(this.props)}
        {renderPagination(this.props)}
      </div>
    );
  }
}

function renderForecastsTable(props) {
  return (
    <table className='table'>
      <thead>
        <tr>
          <th>Name</th>
          <th>MGLT</th>
          <th>ResupplyFrequency</th>
        </tr>
      </thead>
      <tbody>
        {props.forecasts.map(forecast =>
          <tr key={forecast.name}>
            <td>{forecast.name}</td>
            <td>{forecast.mglt}</td>
            <td>{forecast.resupplyFrequency}</td>
          </tr>
        )}
      </tbody>
    </table>
  );
}
function renderPagination(props) 
{
  return <p className='clearfix text-center'>
    {props.isLoading ? <span>Loading...</span> : []}
  </p>;
}

export default connect(
  state => state.swapiForecasts,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(FetchData);
