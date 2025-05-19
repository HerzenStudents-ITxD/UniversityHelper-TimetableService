import { useQuery } from '@tanstack/react-query';
import { format } from 'date-fns';
import { ru } from 'date-fns/locale';
import { getTimetableForGroup } from '../api';
import type { Group } from '../types';

interface GroupTimetableProps {
  group: Group;
  startDate: Date;
  endDate: Date;
}

export function GroupTimetable({ group, startDate, endDate }: GroupTimetableProps) {
  const { data: subjects, isLoading, error } = useQuery({
    queryKey: ['timetable', group.id, startDate, endDate],
    queryFn: () =>
      getTimetableForGroup(
        group.id,
        format(startDate, 'yyyy-MM-dd'),
        format(endDate, 'yyyy-MM-dd')
      ),
  });

  if (isLoading) {
    return <div className="flex justify-center items-center h-64">Загрузка...</div>;
  }

  if (error) {
    return (
      <div className="flex flex-col justify-center items-center h-64 text-herzenBlue">
        <svg xmlns="http://www.w3.org/2000/svg" className="h-16 w-16 mb-4" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 8v4m0 4h.01M21 12c0 4.97-4.03 9-9 9s-9-4.03-9-9 4.03-9 9-9 9 4.03 9 9z" /></svg>
        <span className="text-xl font-semibold">Отсутствует соединение с интернетом</span>
      </div>
    );
  }

  return (
    <div className="container mx-auto px-4 py-8">
      <h1 className="text-2xl font-bold mb-6 text-herzenBlue">
        Расписание для группы {group.group} ({group.institute})
      </h1>
      <div className="bg-white rounded-lg shadow-md overflow-hidden">
        <table className="min-w-full divide-y divide-herzenLightBlue">
          <thead className="bg-herzenLightBlue">
            <tr>
              <th className="px-6 py-3 text-left text-xs font-medium text-herzenBlue uppercase tracking-wider">
                Дата
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-herzenBlue uppercase tracking-wider">
                Предмет
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-herzenBlue uppercase tracking-wider">
                Преподаватель
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-herzenBlue uppercase tracking-wider">
                Аудитория
              </th>
            </tr>
          </thead>
          <tbody className="bg-white divide-y divide-herzenGray">
            {subjects?.map((subject) => (
              <tr key={subject.id} className="hover:bg-herzenLightBlue">
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-700">
                  {format(new Date(subject.date), 'EEEE, d MMMM HH:mm', { locale: ru })}
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-herzenBlue">
                  {subject.name}
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-700">
                  {subject.professor}
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-700">
                  {subject.place}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
} 