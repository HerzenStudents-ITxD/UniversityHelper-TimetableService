import React, { useState, useEffect } from 'react';
import { format, startOfWeek, addDays } from 'date-fns';
import { ru } from 'date-fns/locale';
import { timetableApi } from '../api/timetable';
import type { Subject } from '../api/timetable';

interface TimetableProps {
  groupId?: string;
}

export const Timetable: React.FC<TimetableProps> = ({ groupId }) => {
  const [currentWeek, setCurrentWeek] = useState<Date[]>([]);
  const [subjects, setSubjects] = useState<Subject[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const today = new Date();
    const weekStart = startOfWeek(today, { weekStartsOn: 1 });
    const week = Array.from({ length: 7 }, (_, i) => addDays(weekStart, i));
    setCurrentWeek(week);
  }, []);

  useEffect(() => {
    if (groupId) {
      setLoading(true);
      setError(null);
      timetableApi.getTimetable(groupId)
        .then(data => {
          setSubjects(data);
          setLoading(false);
        })
        .catch(() => {
          setError('Ошибка при загрузке расписания');
          setLoading(false);
        });
    }
  }, [groupId]);

  if (!groupId) {
    return (
      <div className="flex flex-col items-center justify-center h-screen bg-white">
        <img src="/src/assets/icons/no-group.svg" alt="Нет группы" className="w-24 h-24 mb-4" />
        <p className="text-gray text-lg font-inter">Выберите группу для просмотра расписания</p>
      </div>
    );
  }

  if (error) {
    return (
      <div className="flex flex-col items-center justify-center h-screen bg-white">
        <img src="/src/assets/icons/no-internet.svg" alt="Нет интернета" className="w-24 h-24 mb-4" />
        <p className="text-gray text-lg font-inter">{error}</p>
      </div>
    );
  }

  const getSubjectsForDay = (dayIndex: number) => {
    return subjects.filter(subject => subject.dayOfWeek === dayIndex);
  };

  return (
    <div className="flex flex-col h-full bg-white">
      {/* Верхняя неделя */}
      <div className="flex justify-between px-4 py-3 bg-primary">
        {currentWeek.map((day) => (
          <div key={day.toISOString()} className="flex flex-col items-center">
            <span className="text-white text-sm font-roboto">
              {format(day, 'EEE', { locale: ru })}
            </span>
            <span className="text-white text-lg font-roboto font-bold">
              {format(day, 'd')}
            </span>
          </div>
        ))}
      </div>

      {/* Расписание */}
      <div className="flex-1 overflow-y-auto p-4">
        {loading ? (
          <div className="flex justify-center items-center h-full">
            <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-primary"></div>
          </div>
        ) : (
          <div className="space-y-4">
            {currentWeek.map((day, dayIndex) => {
              const daySubjects = getSubjectsForDay(dayIndex);
              return (
                <div key={day.toISOString()} className="space-y-2">
                  <h3 className="text-lg font-inter font-bold text-primary">
                    {format(day, 'EEEE, d MMMM', { locale: ru })}
                  </h3>
                  {daySubjects.length === 0 ? (
                    <p className="text-gray text-sm font-inter">Нет занятий</p>
                  ) : (
                    daySubjects.map(subject => (
                      <div
                        key={subject.id}
                        className="bg-white rounded-lg shadow p-4 border border-gray-200"
                      >
                        <div className="flex justify-between items-start mb-2">
                          <h4 className="text-base font-inter font-bold text-primary">
                            {subject.name}
                          </h4>
                          <span className="text-sm font-roboto text-gray">
                            {subject.startTime} - {subject.endTime}
                          </span>
                        </div>
                        <div className="flex justify-between text-sm font-inter text-gray">
                          <span>{subject.teacher}</span>
                          <span className="flex items-center gap-1">
                            <img src="/src/assets/icons/Vector.svg" alt="Местоположение" className="w-4 h-4" />
                            {subject.room}
                          </span>
                        </div>
                      </div>
                    ))
                  )}
                </div>
              );
            })}
          </div>
        )}
      </div>
    </div>
  );
}; 