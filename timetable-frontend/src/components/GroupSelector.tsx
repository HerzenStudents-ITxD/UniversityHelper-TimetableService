import React, { useState, useEffect } from 'react';
import type { ChangeEvent } from 'react';
import { timetableApi, type Group } from '../api/timetable';

interface GroupSelectorProps {
  onSelect: (groupId: string) => void;
  selectedGroupId?: string;
}

export const GroupSelector: React.FC<GroupSelectorProps> = ({ onSelect, selectedGroupId }) => {
  const [groups, setGroups] = useState<Group[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    setLoading(true);
    setError(null);
    timetableApi.getGroups()
      .then(data => {
        setGroups(data);
        setLoading(false);
      })
      .catch(err => {
        setError('Ошибка при загрузке списка групп');
        setLoading(false);
      });
  }, []);

  if (loading) {
    return (
      <div className="flex justify-center items-center h-12">
        <div className="animate-spin rounded-full h-6 w-6 border-b-2 border-primary"></div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="text-red-500 text-sm font-inter p-2">
        {error}
      </div>
    );
  }

  return (
    <div className="relative">
      <select
        value={selectedGroupId || ''}
        onChange={(e: ChangeEvent<HTMLSelectElement>) => onSelect(e.target.value)}
        className="w-full p-2 border border-gray-200 rounded-lg font-inter text-primary focus:outline-none focus:ring-2 focus:ring-primary focus:border-transparent"
      >
        <option value="">Выберите группу</option>
        {groups.map(group => (
          <option key={group.id} value={group.id}>
            {group.name}
          </option>
        ))}
      </select>
    </div>
  );
}; 