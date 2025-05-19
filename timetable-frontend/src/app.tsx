import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { GroupsList } from './components/GroupsList';
import { CreateGroupForm } from './components/CreateGroupForm';
import { GroupTimetablePage } from './components/GroupTimetablePage';
import { CreateSubjectPage } from './components/CreateSubjectPage';

const queryClient = new QueryClient();

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <Router>
        <div className="min-h-screen bg-gray-100">
          <nav className="bg-white shadow-sm">
            <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
              <div className="flex justify-between h-16">
                <div className="flex">
                  <div className="flex-shrink-0 flex items-center">
                    <Link to="/" className="text-xl font-bold text-indigo-600">
                      Timetable Service
                    </Link>
                  </div>
                  <div className="hidden sm:ml-6 sm:flex sm:space-x-8">
                    <Link
                      to="/"
                      className="border-transparent text-gray-500 hover:border-gray-300 hover:text-gray-700 inline-flex items-center px-1 pt-1 border-b-2 text-sm font-medium"
                    >
                      Groups
                    </Link>
                    <Link
                      to="/create-group"
                      className="border-transparent text-gray-500 hover:border-gray-300 hover:text-gray-700 inline-flex items-center px-1 pt-1 border-b-2 text-sm font-medium"
                    >
                      Create Group
                    </Link>
                  </div>
                </div>
              </div>
            </div>
          </nav>

          <main className="py-10">
            <div className="max-w-7xl mx-auto sm:px-6 lg:px-8">
              <Routes>
                <Route path="/" element={<GroupsList />} />
                <Route path="/create-group" element={<CreateGroupForm />} />
                <Route path="/groups/:groupId/timetable" element={<GroupTimetablePage />} />
                <Route path="/groups/:groupId/subjects/create" element={<CreateSubjectPage />} />
              </Routes>
            </div>
          </main>
        </div>
      </Router>
    </QueryClientProvider>
  );
}

export default App;
