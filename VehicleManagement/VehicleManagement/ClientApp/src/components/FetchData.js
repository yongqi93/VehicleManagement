import React, { Component } from 'react';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { cars: [], loading: true };
  }

  componentDidMount() {
    this.populateCarData();
  }

  static renderCarsTable(cars) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Make</th>
            <th>Model</th>
            <th>Engine</th>
                    <th>BodyType</th>
          </tr>
        </thead>
        <tbody>
                {cars.map(car =>
             <tr key={car.id}>
                        <td>{car.Make}</td>
                        <td>{car.Model}</td>
                        <td>{car.Engine}</td>
                        <td>{car.BodyType}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
        ? <p><em>Loading...</em></p>
        : FetchData.renderCarsTable(this.state.cars);

    return (
      <div>
        <h1 id="tabelLabel" >Cars</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateCarData() {
    const response = await fetch('car');
    const data = await response.json();
    this.setState({ cars: data, loading: false });
  }
}
