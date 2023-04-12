import logo from './logo.svg';
import './App.css';
import router from './router/router';
import {
    RouterProvider,
} from "react-router-dom";

function App() {
  return (
    <div>
          <RouterProvider router={router} />
    </div>
  );
}

export default App;
