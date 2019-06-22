const requestSwapiForecastsType = 'REQUEST_SWAPI';
const receiveSwapiForecastsType = 'RECEIVE_SWAPI';
const initialState = { forecasts: [], isLoading: false };
const APIUrlBase = `http://10.1.0.5/api/`
const headers = {
  method: 'GET',
  headers: {
  "Access-Control-Allow-Origin":"*",
  'Cache-Control': 'no-cache',
  'CrossDomain': 'true'
  },
  }

export const actionCreators = {
  requestSwapiForecasts: distance => async (dispatch, getState) => {    
    if (distance === getState().swapiForecasts.distance) {
      // Don't issue a duplicate request (we already have or are loading the requested data)
      return;
    }
    else
    {
  
      const url = `${APIUrlBase}starship/ResupplyCalc?distance=${distance}`;
      const response = await fetch(url, headers);
      const forecasts = await response.json();
  
      dispatch({ type: receiveSwapiForecastsType, distance, forecasts });
    }
  },

  requestSwapiLoadData: json => async (dispatch) => 
  {  
    const urlCount = `${APIUrlBase}starship`;
    const responseCount = await fetch(urlCount, headers);
    const starships = await responseCount.json();
    // let starships = 0;
    if (starships == null || starships.length <= 0)
    {
      const url = `${APIUrlBase}starship/loaddata`;
      const response = await fetch(url, headers);
      const forecasts = await response.json();
  
      dispatch({ type: receiveSwapiForecastsType, forecasts });
    }
  },

  requestSwapiClearData: json => async (dispatch) => {
    
    const url = `${APIUrlBase}starship/cleardata`;
    const response = await fetch(url, headers);
    const forecasts = await response.json();

    dispatch({ type: receiveSwapiForecastsType, forecasts });
  }
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === requestSwapiForecastsType) {
    return {
      ...state,
      distance: action.distance,
      isLoading: true
    };
  }

  if (action.type === receiveSwapiForecastsType) {
    return {
      ...state,
      distance: action.distance,
      forecasts: action.forecasts,
      isLoading: false
    };
  }

  return state;
};
