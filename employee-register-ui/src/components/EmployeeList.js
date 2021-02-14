import React from 'react';
import Employee from "./Employee";
import axios from 'axios'
export default function EmployeeList() {
    const employeeApi = (url = 'https://localhost:5001/api/Employees/') => {
        return {
            fetchAll: () => axios.get(url),
            create: newRecord => axios.post(url, newRecord),
            update: (id, updatedRecord) => axios.put(url+id, updatedRecord),
            delete: id => axios.delete(url+id)
        }
    }
    const addOrEdit = (formData, onSuccess) => {
        employeeApi().create(formData)
            .then(res => {
                onSuccess();
            })
            .catch(err => console.error(err));
    }
    return (
        <div className="row">
            <div className="col-md-12">
                <div className="jumbotron jumbotron-fluid py-4">
                    <div className="container text-center">
                        <h1 className="display-4">Employee Register</h1>
                    </div>
                </div>
            </div>
            <div className="col-md-4">
                <Employee
                    addOrEdit={addOrEdit}
                />
            </div>
            <div className="col-md-8">
                <div>List of Employee Records</div>
            </div>
        </div>
    )
}
