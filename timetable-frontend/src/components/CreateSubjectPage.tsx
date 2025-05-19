import { useParams } from 'react-router-dom';
import { CreateSubjectForm } from './CreateSubjectForm';

export function CreateSubjectPage() {
  const { groupId } = useParams<{ groupId: string }>();

  if (!groupId) {
    return (
      <div className="flex justify-center items-center h-64 text-red-500">
        Group ID is required
      </div>
    );
  }

  return <CreateSubjectForm groupId={groupId} />;
} 