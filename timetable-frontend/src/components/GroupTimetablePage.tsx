import { useParams } from 'react-router-dom';
import { useQuery } from '@tanstack/react-query';
import { getGroupById } from '../api';
import { GroupTimetable } from './GroupTimetable';

export function GroupTimetablePage() {
  const { groupId } = useParams<{ groupId: string }>();
  const { data: group, isLoading, error } = useQuery({
    queryKey: ['group', groupId],
    queryFn: () => getGroupById(groupId!),
    enabled: !!groupId,
  });

  if (isLoading) {
    return <div className="flex justify-center items-center h-64">Loading...</div>;
  }

  if (error || !group) {
    return (
      <div className="flex justify-center items-center h-64 text-red-500">
        Error loading group
      </div>
    );
  }

  const startDate = new Date();
  const endDate = new Date();
  endDate.setDate(endDate.getDate() + 7); // Show next 7 days

  return <GroupTimetable group={group} startDate={startDate} endDate={endDate} />;
} 