import AdminLayout from './layout/AdminLayout';
import DashboardContent from './pages/DashboardContent';
import ThemeProvider from './theme/ThemeProvider';

function App() {
  return (
    <ThemeProvider>
      <AdminLayout>
        <DashboardContent />
      </AdminLayout>
    </ThemeProvider>
  );
}

export default App;
