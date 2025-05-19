import { useQuery } from '@tanstack/react-query';
import { Link } from 'react-router-dom';
import { getAllGroups } from '../api';
import type { Group } from '../types';

export function GroupsList() {
  const { data: groups, isLoading, error } = useQuery<Group[]>({
    queryKey: ['groups'],
    queryFn: getAllGroups,
  });

  if (isLoading) {
    return <div className="flex justify-center items-center h-64">Loading...</div>;
  }

  if (error) {
    return (
      <div className="flex justify-center items-center h-64 text-red-500">
        Error loading groups
      </div>
    );
  }

  return (
    <div className="container mx-auto px-4 py-8">
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold">Groups</h1>
        <Link
          to="/create-group"
          className="inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md shadow-sm text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
        >
          Create New Group
        </Link>
      </div>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
        {groups?.map((group) => (
          <div
            key={group.id}
            className="bg-white rounded-lg shadow-md p-6 hover:shadow-lg transition-shadow"
          >
            <h2 className="text-xl font-semibold mb-2">{group.group}</h2>
            <div className="space-y-2 text-gray-600">
              <p>
                <span className="font-medium">Institute:</span> {group.institute}
              </p>
              <p>
                <span className="font-medium">Faculty:</span> {group.faculcity}
              </p>
              <p>
                <span className="font-medium">Degree:</span> {group.degree}
              </p>
              <p>
                <span className="font-medium">Course:</span> {group.course}
              </p>
              <p>
                <span className="font-medium">Direction:</span> {group.direction}
              </p>
              <p>
                <span className="font-medium">Subgroup:</span> {group.subGroup}
              </p>
            </div>
            <div className="mt-4 flex space-x-4">
              <Link
                to={`/groups/${group.id}/timetable`}
                className="inline-flex items-center px-3 py-2 border border-transparent text-sm leading-4 font-medium rounded-md text-indigo-700 bg-indigo-100 hover:bg-indigo-200 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
              >
                View Timetable
              </Link>
              <Link
                to={`/groups/${group.id}/subjects/create`}
                className="inline-flex items-center px-3 py-2 border border-transparent text-sm leading-4 font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
              >
                Add Subject
              </Link>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
} 